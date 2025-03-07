using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // An Entity is an object in the game with a position
    public abstract class Entity
    {
        public virtual Vector2 Position {get{return position;} set{position = value;}}
        protected Vector2 position;
        public Entity(Vector2 position) {
            this.position = position;
        }

        // Require subclasses to inherit Update and Draw
        public abstract void Update(Game game);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
