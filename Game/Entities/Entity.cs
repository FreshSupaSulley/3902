using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // An Entity is an object in the game with a position
    public abstract class Entity(Vector2 position)
    {
        public virtual Vector2 Position {get; set;} = position;
        private Vector2 position;

        // Require subclasses to inherit Update and Draw
        public abstract void Update(Game game);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
