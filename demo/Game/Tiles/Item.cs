using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;

namespace Game.Tiles
{
    public class Item : ITile
    {
        private static readonly Sprite Heart = new Sprite(Game.Load("/Items/heart.png"));

        public Item()
        {

        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch batch)
        {
            Heart.Draw(batch, new Vector2(100, 200));
        }
    }
}
