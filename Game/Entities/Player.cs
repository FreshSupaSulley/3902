using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Game.Items;
using Game.Controllers;
using Microsoft.Xna.Framework.Input;

namespace Game.Entities
{
	public class Player : MobileEntity
	{
		private static readonly int ANIMATION_SPEED = 10;
		private static readonly Texture2D WALK_SHEET = Game.Load("Entities/Monoko/walk.png");

		// Static animations monoko can switch between
		private static readonly Animation IDLE = new(Game.Subimage(WALK_SHEET, new Rectangle(24, 64, 24, 32)), 1, ANIMATION_SPEED);
		private static readonly Animation UP = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 0, 96, 32)), 4, ANIMATION_SPEED);
		private static readonly Animation RIGHT = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 32, 96, 32)), 4, ANIMATION_SPEED);
		private static readonly Animation DOWN = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 64, 96, 32)), 4, ANIMATION_SPEED);
		private static readonly Animation LEFT = new(Game.Subimage(WALK_SHEET, new Rectangle(0, 96, 96, 32)), 4, ANIMATION_SPEED);

		// Attack
		private static readonly Animation ATTACK = new(Game.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);
		// Old damaged sprite doesn't fit same style. Needs new resource
		private static readonly Animation DAMAGE = new(Game.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED);

		public Item Item;
		public Player() : base(new Vector2(100, 100), IDLE) { }

		public override void Update(Game game)
		{
			Move(game.keyboard);
			base.Update(game);

			// Using items
			if (Item is not null)
			{
				Item.Position = base.Position;
			}
		}

		private void Move(KeyboardController keyboard)
		{
			// If attacking, don't move
			if (keyboard.IsKeyDown(Keys.Z, Keys.N))
			{
				TempBuffer.add(new TempEntity(TempBuffer.pow, Position), 1000);
				ActiveAnimation = ATTACK;
				return;
			}
			// Movement
			// You could get rid of the normalization to make movement look smoother
			const int speed = 1;
			Vector2 velocity = new Vector2();
			if (keyboard.IsKeyDown(Keys.W, Keys.Up)) velocity += new Vector2(0, -1);
			if (keyboard.IsKeyDown(Keys.A, Keys.Left)) velocity += new Vector2(-1, 0);
			if (keyboard.IsKeyDown(Keys.S, Keys.Down)) velocity += new Vector2(0, 1);
			if (keyboard.IsKeyDown(Keys.D, Keys.Right)) velocity += new Vector2(1, 0);
			// Use velocity to determine animation
			if (velocity == Vector2.Zero)
			{
				ActiveAnimation = IDLE;
			}
			else
			{
				// Normalize to keep consistent speed
				velocity.Normalize();
				// Check directions
				// No diagonal sprites so this will have to suffice
				if (velocity.X > 0) ActiveAnimation = RIGHT;
				else if (velocity.X < 0) ActiveAnimation = LEFT;
				else if (velocity.Y > 0) ActiveAnimation = DOWN;
				else if (velocity.Y < 0) ActiveAnimation = UP;
			}
			Position += velocity * speed;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			// Draw item if exists
			Item?.Draw(spriteBatch);
		}
	}
}
