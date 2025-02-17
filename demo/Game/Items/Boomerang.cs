using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Boomerang : IItem
    {
        private static readonly Sprite SPRITE = new Sprite(Game.Load("/Items/boomerang.png"));

        public override void Use() => throw new System.NotImplementedException();

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
