using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using Game.Graphics;
using Microsoft.Xna.Framework;

namespace demo.Game.Entities
{
    //Temporary entity, an entity that appears on screen for a set amount of time and then disappears
    internal class TempEntity : Entity
    {
        private Texture2D texture;
        float opacity = 1.0f;
        System.Numerics.Vector2 position;
        public TempEntity(Texture2D txt, System.Numerics.Vector2 v) : base(v) {
            texture = txt;
            position = v;
        }
        public override void Update()
        {
            opacity *= 0.9f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White * opacity);
        }

    }
}
