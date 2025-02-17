﻿using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Game.Items;

namespace Game.Entities
{
    public class Player : MobileEntity
	{
		// Static animations monoko can switch between
		private static readonly Animation mkUp = new Animation(Monoko.monoko, Monoko.mkBack);
		private static readonly Animation mkDown = new Animation(Monoko.monoko, Monoko.mkFront);
		private static readonly Animation mkLeft = new Animation(Monoko.monoko, Monoko.mkLeft);
		private static readonly Animation mkRight = new Animation(Monoko.monoko, Monoko.mkRight);

		public enum srcSprites
		{
			UP = 0,
			DOWN = 1,
			LEFT = 2,
			RIGHT = 3,
			ATTACK = 4,
			DAMAGED = 5
		};
		public enum Facing
		{
			VAN = 0,
			REAR = 1,
			WEST = 2,
			EAST = 3
		}

		public Dictionary<srcSprites, Animation> animationSequences { get; set; }
		public Dictionary<srcSprites, Animation> damaged { get; set; }
		public Dictionary<Facing, Animation> attack { get; set; }
		private Animation prev;
		public bool attackFlag { get; set; } = false;
		public int speed { get; set; } = 1;
		public Facing direction = Facing.VAN;
		private bool moving;
		public Item Item;

		public Player() : base(new System.Numerics.Vector2(Monoko.spawnX, Monoko.spawnY), new Animation(Monoko.monoko, Monoko.mkAll))
		{
			animationSequences = new Dictionary<srcSprites, Animation>();
			animationSequences.Add(srcSprites.UP, mkUp);
			animationSequences.Add(srcSprites.DOWN, mkDown);
			animationSequences.Add(srcSprites.LEFT, mkLeft);
			animationSequences.Add(srcSprites.RIGHT, mkRight);
			animationSequences.Add(srcSprites.ATTACK, new Animation(Monoko.monoko, new Rectangle[] { Monoko.scaryDefault }));
			animationSequences.Add(srcSprites.DAMAGED, new Animation(Monoko.monoko, new Rectangle[] { Monoko.mkEmotionallyDamaged }));
		}

		public void animate(int direction, int orientation)
		{
			prev = this.activeAnimation;
			moving = true;
			if (direction > 0) //increasing value
			{
				base.activeAnimation = orientation == 0 ? animationSequences[srcSprites.RIGHT] : animationSequences[srcSprites.DOWN];
				this.direction = orientation == 0 ? Facing.EAST : Facing.VAN;
			}
			else //decreasing value
			{
				base.activeAnimation = orientation == 0 ? animationSequences[srcSprites.LEFT] : animationSequences[srcSprites.UP];
				this.direction = orientation == 0 ? Facing.WEST : Facing.REAR;
			}
		}

		//directly change currentAnimation
		public void animate(int[] anim)
		{
			moving = true;
			prev = this.activeAnimation;
			Rectangle[] temp = new Rectangle[anim.Length];
			for (int i = 0; i < anim.Length; i++)
			{
				temp[i] = Monoko.mkAll[anim[i]];
			}
			this.activeAnimation = new Animation(Monoko.monoko, temp);
		}

		//directly change currentAnimation #2
		public void animate(Animation a)
		{
			moving = true;
			this.activeAnimation = a;
		}
		//Change current animation to previous animation 
		public void restore()
		{
			if (prev != null)
			{
				this.activeAnimation = prev;
			}
		}
		public void fillAttack(Dictionary<Facing, Animation> a)
		{
			this.attack = a;
			attackFlag = true;
		}
		public void enforceDimensions()
		{
			this.activeAnimation.manualOverride(Monoko.mkWidth, Monoko.mkHeight);
		}

		public void UseItem(Game game)
		{
			if(Item is not null)
			{
				game.AddGameObject(Item);
				Item = null;
			}
		}

		public override void Update()
		{
			// Using items
			if (Item is not null)
			{
				Item.Position = base.Position;
			}
			if (moving)
			{
				base.activeAnimation.Update();
			}
			else
			{
				base.activeAnimation.Reset();
			}

			moving = false;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw item
			if (Item is not null)
			{
				Item.Draw(spriteBatch);
			}
		}
	}
}
