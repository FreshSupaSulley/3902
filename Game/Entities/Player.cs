using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Game.Items;
using Game.Controllers;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using Game.State;
using Game.Util;
using System.Diagnostics;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.KeyResponses;

namespace Game.Entities
{
    public class Player : LivingEntity
	{

		//maybe this could be outsourced into an xml file?
		public static Dictionary<string, Keys> left_map = new Dictionary<string, Keys>(){
			{"up", Keys.W},
			{"down", Keys.S},
			{"right", Keys.D},
			{"left", Keys.A},
			{"attack", Keys.Z},
			{"heart", Keys.D1},
			{"banana", Keys.D2},
			{"bomb", Keys.D3}
		};


		public static Dictionary<string, Keys> right_map = new Dictionary<string, Keys>(){
			{"up", Keys.Up},
			{"down", Keys.Down},
			{"right", Keys.Right},
			{"left", Keys.Left},
			{"attack", Keys.N},
			{"heart", Keys.D8},
			{"banana", Keys.D9},
			{"bomb", Keys.D0}
		};
		private static readonly int ANIMATION_SPEED = 8;
		private static readonly Texture2D WALK_SHEET = Main.Load("Entities/Monoko/walk.png");

		private static float scale = 0.7f;

		// Walk animations
		public static readonly Animation UP = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 0, 96, 32)), 4, ANIMATION_SPEED, scale);
		public static readonly Animation RIGHT = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 32, 96, 32)), 4, ANIMATION_SPEED, scale);
		public static readonly Animation DOWN = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 64, 96, 32)), 4, ANIMATION_SPEED, scale);
		public static readonly Animation LEFT = new(Main.Subimage(WALK_SHEET, new Rectangle(0, 96, 96, 32)), 4, ANIMATION_SPEED, scale);

		// Attack
		public static readonly Animation ATTACK = new(Main.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED, scale);
		// Old damaged sprite doesn't fit same style. Needs new resource
		private static readonly Animation DAMAGE = new(Main.Load("Entities/Monoko/attack.png"), 3, ANIMATION_SPEED, scale);

		// Dead
		private static readonly Sprite HEAD = new(Main.Load("Entities/Monoko/head.png")), BODY = new(Main.Load("Entities/Monoko/body.png"));

		//item carrying
		private static int Key = 0;
        private static int rupee = 0;
        private static int bomb = 0;

		private static readonly int TOTAL_DEATH_TICKS = 120, I_FRAMES = 30;
		private bool invulnerable, dead;
		private int deadTicks, iframeTicks;

		[XmlIgnore] // required??
		public Item Item;
		// Player has 100hp

		[XmlIgnore]
        public Dictionary<Keys, IKeyResponse> mapping = new Dictionary<Keys, IKeyResponse>();

		[XmlIgnore]
		public Animation ownUp, ownDown, ownLeft, ownRight, ownAttack;

		public Player() : base(
			100, 
			new(
				(int)(BODY.Texture.Width*scale)/2-7, 
				(int)((BODY.Texture.Height)*scale)/2-7, 
				14, 
				14
			), 
			DOWN
			) {
				this.ownUp = Player.UP.makeCopy();
				this.ownRight = Player.RIGHT.makeCopy();
				this.ownDown = Player.DOWN.makeCopy();
				this.ownLeft = Player.LEFT.makeCopy();
				this.ownAttack = Player.ATTACK.makeCopy();
		 }


        public int GetKey() => Key;
		public int GetRupee() => rupee;
		public int GetBomb() => bomb;

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
					Main.uiManager.ChangeUIState("death");
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
			//return HandleInputs(game);
			Vector2 req = new Vector2(0, 0);
			KeyboardState ks = Keyboard.GetState();
			Keys[] pressed = ks.GetPressedKeys();
			int mappedKeyCount = 0;
			for(int i = 0; i < pressed.Length; i++){
				if(this.mapping.ContainsKey(pressed[i])){
					mappedKeyCount++;
					this.mapping[pressed[i]].processGame(game);
					req += this.mapping[pressed[i]].respond();
				}
			}
			if(mappedKeyCount == 0){
				base.ActiveAnimation.Reset(); //remain still if no mapped key is pressed
			}
			return req;
		}

		public bool HasKey()
		{
            if (Key >= 1)
            {
				return true;
            }
			return false;
		}

		public void addKey()
		{
			Key++;
		}

		public void useKey(){
			Key--;
		}
		
		public void setKey(int count){
			Key = count;
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
			if (Main.debug) {
                Rectangle temp = new Rectangle(
                    (int) (collisionBox.X + Position.X), 
                    (int) (collisionBox.Y + Position.Y), 
                    collisionBox.Width, 
                    collisionBox.Height);
				DebugTools.DrawRect(spriteBatch, temp, new Color(Color.Green, 0.5f));
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
				if(base.health < 50){
					game.sfx["pain"].Play();
				}
			}
		}

		public override void OnDeath(State.Game game)
		{
			dead = true;
		}

		public void makeMappings(Dictionary<string, Keys> dict){
				this.mapping.TryAdd(dict["up"], new MoveResponse(this, 0, -1));
				this.mapping.TryAdd(dict["left"], new MoveResponse(this, -1, 0));
				this.mapping.TryAdd(dict["down"], new MoveResponse(this, 0, 1));
				this.mapping.TryAdd(dict["right"], new MoveResponse(this, 1, 0));
				this.mapping.TryAdd(dict["attack"], new ExertResponse(this));
				this.mapping.TryAdd(dict["heart"], new AcquireResponse(this, new Heart(base.Position)));
				this.mapping.TryAdd(dict["banana"], new AcquireResponse(this, new Banana(base.Position)));
				this.mapping.TryAdd(dict["bomb"], new AcquireResponse(this, new Bomb(base.Position)));
		}
	}
}
