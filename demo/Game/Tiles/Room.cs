using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Numerics;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;
using System.Net.Mime;
using Game.Entities;

namespace Game.Tiles
{
    public class Room : IGameObject
    {
        private static readonly Texture2D Wall = Game.Load("Tiles/room.png");

        public void Update()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Wall, new System.Numerics.Vector2(0, 0), Color.White);
        }

    }
}
