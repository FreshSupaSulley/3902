using System.Xml.Serialization;
using Game.Items;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // An Entity is an object in the game with a position
    // List every single deserializable entity we want in this game
    // I don't know of a better way to handle deserialization. Anyone else is welcome to find an alternative
    [XmlInclude(typeof(Player))]
    [XmlInclude(typeof(Dragon))]
    [XmlInclude(typeof(Skeleton))]
    [XmlInclude(typeof(Wallmaster))]
    [XmlInclude(typeof(Goriya))]
    [XmlInclude(typeof(Trap))]
    [XmlInclude(typeof(Gel))]
    [XmlInclude(typeof(Bat))]
    [XmlInclude(typeof(NPC))]
    [XmlInclude(typeof(Key))]
    [XmlInclude(typeof(WinStar))]
    public abstract class Entity(Vector2 position)
    {
        public Vector2 Position = position;

        // Require subclasses to inherit Update and Draw
        public abstract void Update(State.Game game);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
