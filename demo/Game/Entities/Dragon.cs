using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;

namespace Game.Entities
{
    public class Dragon : MobileEntity
    {
        private static readonly Animation IDLE = new Animation(Game.Load("/Dragon/dragon.png"), 4, 10);

        public Dragon() : base(new Vector2(Monoko.spawnX, Monoko.spawnY) + new Vector2(100, 100), IDLE) {}
    }
}
