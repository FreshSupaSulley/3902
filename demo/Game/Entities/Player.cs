using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;

public class Player : MobileEntity
{
	// Static animations monoko can switch between
	private static readonly Animation UP = new Animation(Globals.monoko, Globals.mkBack);
	private static readonly Animation DOWN = new Animation(Globals.monoko, Globals.mkFront);
	private static readonly Animation LEFT = new Animation(Globals.monoko, Globals.mkLeft);
	private static readonly Animation RIGHT = new Animation(Globals.monoko, Globals.mkRight);
	public bool attackFlag { get; set; } = false;
	public int speed { get; set; } = 1;
	private bool moving;

	public Player() : base(new System.Numerics.Vector2(Globals.spawnX, Globals.spawnY), new Animation(Globals.monoko, Globals.mkAll)) { }

	public void animate(int direction, int orientation)
	{
		moving = true;
		if (direction > 0) //increasing value
		{
			base.activeAnimation = orientation == 0 ? RIGHT : DOWN;
		}
		else //decreasing value
		{
			base.activeAnimation = orientation == 0 ? LEFT : UP;
		}
	}

	//directly change currentAnimation
	public void animate(int[] anim)
	{
		Rectangle[] temp = new Rectangle[anim.Length];
		for(int i = 0; i < anim.Length; i++)
		{
			temp[i] = Globals.mkAll[anim[i]];
		}
		this.activeAnimation = new Animation(Globals.monoko, temp);
	}

	public override void Update()
	{
		if (moving)
		{
			base.Update();
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
	}
}
