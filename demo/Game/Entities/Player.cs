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

public class PlayerCharacter : MobileEntity
{
	public int speed { get; set; }
	private Dictionary<String, int[]> directionalAnimations;
	public PlayerCharacter() : base(new System.Numerics.Vector2(Specs_h.spawnX, Specs_h.spawnY), new Animation(Specs_h.monoko, Specs_h.mkAll))
	{
		speed = 15;
		directionalAnimations = new Dictionary<String, int[]>();
		directionalAnimations.Add("right", Specs_h.mkRightIndex);
		directionalAnimations.Add("left", Specs_h.mkLeftIndex);
		directionalAnimations.Add("up", Specs_h.mkUp);
		directionalAnimations.Add("down", Specs_h.mkDown);
	}

	public override void Update()
	{

	}

	public void animate(int direction, int orientation)
	{
		if(direction > 0) //increasing value
		{
			if(orientation == 0) //increasing x-value (moving right)
			{
				base.activeAnimation.currentAnimation = directionalAnimations["right"];
			}
			else //increasing y-value (moving down)
			{
				base.activeAnimation.currentAnimation = directionalAnimations["down"];
			}
		}
		else //decreasing value
		{
			if(orientation == 0) //decreasing x-vaue (moving left)
			{
				base.activeAnimation.currentAnimation = directionalAnimations["left"];
			}
			else //decreasing y-value (moving up)
			{
				base.activeAnimation.currentAnimation = directionalAnimations["up"];
			}

		}
	}

	public override void Draw(SpriteBatch spriteBatch)
	{
		base.Draw(spriteBatch);
	}
}
