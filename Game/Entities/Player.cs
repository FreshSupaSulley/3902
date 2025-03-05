using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Game.Items;
using Game.Controllers;
using Microsoft.Xna.Framework.Input;
using Game.Collision;

namespace Game.Entities
{
	public class Player : LivingEntity
	{
		private static readonly int ANIMATION_SPEED = 8;
		private static readonly Texture2D WALK_SHEET = Game.Load("Entities/Monoko/walk.png");

		// Walk animations
		public static readonly Animation UP = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 0, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation RIGHT = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 32, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation DOWN = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 64, 96, 32)), 4, ANIMATION_SPEED);
		public static readonly Animation LEFT = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 96, 96, 32)), 4, ANIMATION_SPEED);

		// Attack
		private static readonly Animation ATTACK = new(Game.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);
		// Old damaged sprite doesn't fit same style. Needs new resource
		private static readonly Animation DAMAGE = new(Game.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);

		// Could prove useful one day
		public bool Moving { get; private set; }

		public Item Item;
		public Player() : base(new(5, 13, 14, 14), new Vector2(60, 80), DOWN) { }

		public override Vector2 Move(Game game)
		{
			// Using items
			if (Item is not null)
			{
				Item.Position = base.Position;
			}
			return HandleInputs(game);
		}

		private Vector2 HandleInputs(Game game)
		{
			KeyboardController keyboard = game.keyboard;
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
					TempBuffer.add(new TempEntity(TempBuffer.pow, Position), 1000);
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
				Moving = true;
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
				Moving = false;
			}
			// Position += velocity * speed;
			return velocity * speed;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			// Draw item if exists
			Item?.Draw(spriteBatch);
		}

		public int GetDirection()
		{
			if (ActiveAnimation == UP) return 0;
			if (ActiveAnimation == RIGHT) return 1;
			if (ActiveAnimation == LEFT) return 3;
			// Every other animation is down for now
			return 2;
		}
	}
}
