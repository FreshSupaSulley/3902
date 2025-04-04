using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Game.Items;
using Game.Controllers;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using Game.State;
using Game.Util;
using System;

namespace Game.Entities
{
	public class Player : LivingEntity
	{
		private static readonly int ANIMATION_SPEED = 8;
		private static readonly Texture2D WALK_SHEET = Main.Load("Entities/Monoko/walk.png");

		// Walk animations
		public static readonly Animation UP = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 0, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation RIGHT = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 32, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation DOWN = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 64, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation LEFT = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 96, 96, 32)), 4, ANIMATION_SPEED);

		// Attack
		private static readonly Animation ATTACK = new(Main.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);
		// Old damaged sprite doesn't fit same style. Needs new resource
		private static readonly Animation DAMAGE = new(Main.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);

		// Dead
		private static readonly Sprite HEAD = new(Main.Load("Entities/Monoko/head.png")), BODY = new(Main.Load("Entities/Monoko/body.png"));

		//item carrying
		// there are these awesome things called booleans you should check them out ):
		public static readonly int Key = 1;
		public static readonly int rupee = 0;
		public static readonly int bomb = 0;

		private static readonly int TOTAL_DEATH_TICKS = 120, I_FRAMES = 30;
		private bool invulnerable, dead;
		private int deadTicks, iframeTicks;

		[XmlIgnore] // required??
		public Item Item;

		// Player has 100hp
		public Player() : base(100, new(5, 13, 14, 14), DOWN) { }

		public override void Update(State.Game game)
		{
			base.Update(game);

			// Handle invulnerability
			if (invulnerable && ++iframeTicks >= I_FRAMES)
			{
				invulnerable = false;
			}

			if (dead)
			{
				if (deadTicks++ == 0)
				{
					game.sfx["fart"].Play();
				}
				else if (deadTicks > TOTAL_DEATH_TICKS)
				{
					Main.SwitchGameState(new Death(Main.device));
				}
			}
		}

		public override Vector2 Move(State.Game game)
		{
			// Don't move while dead
			if (dead) return new();
			// Using items
			if (Item is not null)
			{
				Item.Position = base.Position;
			}
			return HandleInputs(game);
		}

		private Vector2 HandleInputs(State.Game game)
		{
			KeyboardController keyboard = Main.INSTANCE.keyboard;
			// Attack
			if (keyboard.IsKeyDown(Keys.Z, Keys.N))
			{
				// If we have an item, use it
				if (Item is not null)
				{
					Item.Use(game);
					Item = null;
				}
				else
				{
					if (ActiveAnimation != ATTACK)
					{
						game.sfx["punch"].Play();
						TempBuffer.add(new TempEntity(TempBuffer.pow, Position), 1000);
						int padding = 30;
						// Widespread area hitbox for testing. Later we want this to be directional
						game.room.AddHitbox(new(10, this, new(-padding, -padding, collisionBox.Width + padding * 2, collisionBox.Height + padding * 2)));
					}
					ActiveAnimation = ATTACK;
				}
				// If attacking, don't move
				return new();
			}
			// Required for Sprint 3
			if (keyboard.IsKeyPressed(Keys.D1)) Item = new Heart(Position);
			else if (keyboard.IsKeyPressed(Keys.D2)) Item = new Boomerang(Position);
			else if (keyboard.IsKeyPressed(Keys.D3)) Item = new Bomb(Position);
			// Movement
			// You could get rid of the normalization to make movement look smoother
			const int speed = 1;
			Vector2 velocity = new();
			if (keyboard.IsKeyDown(Keys.W, Keys.Up)) velocity += new Vector2(0, -1);
			if (keyboard.IsKeyDown(Keys.A, Keys.Left)) velocity += new Vector2(-1, 0);
			if (keyboard.IsKeyDown(Keys.S, Keys.Down)) velocity += new Vector2(0, 1);
			if (keyboard.IsKeyDown(Keys.D, Keys.Right)) velocity += new Vector2(1, 0);
			// Use velocity to determine animation
			if (velocity != Vector2.Zero)
			{
				// Normalize to keep consistent speed
				velocity.Normalize();
				// No diagonal sprites so this will have to suffice
				if (velocity.X > 0) ActiveAnimation = RIGHT;
				else if (velocity.X < 0) ActiveAnimation = LEFT;
				else if (velocity.Y > 0) ActiveAnimation = DOWN;
				else if (velocity.Y < 0) ActiveAnimation = UP;
			}
			else
			{
				ActiveAnimation.Reset();
			}
			if (keyboard.IsKeyPressed(Keys.M))
			{
				game.muteRequest = 1;
			}
			// Position += velocity * speed;
			return velocity * speed;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!dead)
			{
				if (invulnerable)
				{
					base.Draw(spriteBatch, new Color(Color.White, 0.5f));
				}
				else
				{
					base.Draw(spriteBatch);
				}
				// Draw item if exists
				Item?.Draw(spriteBatch);
			}
			else
			{
				// idgaf anymore
				// fart death animation lol
				spriteBatch.Draw(HEAD.Texture, new((int)Position.X + HEAD.Texture.Width / 2, (int)Position.Y + HEAD.Texture.Height / 2 - deadTicks), null, Color.White, MathHelper.ToRadians(deadTicks * 12), new Vector2(HEAD.Texture.Width / 2f, HEAD.Texture.Height / 2f), new Vector2(1), SpriteEffects.None, 0f);
				spriteBatch.Draw(BODY.Texture, new Vector2((int)Position.X, (int)Position.Y), Color.White);
			}
		}

		public int GetDirection()
		{
			if (ActiveAnimation == UP) return 0;
			if (ActiveAnimation == RIGHT) return 1;
			if (ActiveAnimation == LEFT) return 3;
			// Every other animation is down for now
			return 2;
		}

		public override void Inflict(State.Game game, int damage)
		{
			// If we're in iframes don't apply damage
			if (!invulnerable)
			{
				iframeTicks = 0;
				invulnerable = true;
				base.Inflict(game, damage);
			}
		}

		public override void OnDeath(State.Game game)
		{
			dead = true;
		}
	}
}
