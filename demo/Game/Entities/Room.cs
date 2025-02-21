using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;
using System.Net.Mime;

namespace Game.Entities
{
    public class Room : IGameObject
    {
        private static readonly Sprite Wall = new(Game.Load("/Tiles/room.png", new(0, 0, 1169, 1246)));
        private static readonly Sprite DoorTop = new(Game.Load("/Tiles/room.png", new(0, 0, 1169, 1246)));
        private static readonly Sprite DoorRight = new(Game.Load("/Tiles/room.png", new(0, 0, 1169, 1246)));
        private static readonly Sprite DoorBottom = new(Game.Load("/Tiles/room.png", new(0, 0, 1169, 1246)));
        private static readonly Sprite DoorLeft = new(Game.Load("/Tiles/room.png", new(0, 0, 1169, 1246)));

        public void Update()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            Wall.Draw(batch, new Microsoft.Xna.Framework.Vector2(0,0));
        }

    }
}
