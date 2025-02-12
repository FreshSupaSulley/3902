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
using System.Reflection.Metadata;

public class Player : MobileEntity
{
	// Static animations monoko can switch between
	private static readonly Animation UP = new Animation(Globals.monoko, Globals.mkBack);
	private static readonly Animation DOWN = new Animation(Globals.monoko, Globals.mkFront);
	private static readonly Animation LEFT = new Animation(Globals.monoko, Globals.mkLeft);
	private static readonly Animation RIGHT = new Animation(Globals.monoko, Globals.mkRight);
	private bool facingBack;
	public int speed { get; set; } = 1;
	private bool moving;

	public Player() : base(new System.Numerics.Vector2(Globals.spawnX, Globals.spawnY), new Animation(Globals.monoko, Globals.mkAll)) { }

	public void animate(int direction, int orientation)
	{
		moving = true;
		if (direction > 0) //increasing value
		{
			base.activeAnimation.reset(orientation == 0 ? Globals.mkRightIndex : Globals.mkDown);
		}
		else //decreasing value
		{
			base.activeAnimation.reset(orientation == 0 ? Globals.mkLeftIndex : Globals.mkUp);
		}
        if (direction == 0)
        {
            if (facingBack)
            {
                base.activeAnimation.reset(new int[] { 11 });
            }
            else
            {
                base.activeAnimation.reset(new int[] { 0 });
            }
        }
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
