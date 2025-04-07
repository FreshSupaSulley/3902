using Game.Entities;
using Game.State;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Game.Items
{
    public abstract class Item(Vector2 position) : Entity(position)
    {

        [XmlInclude(typeof(Key))]
        /// Item was used
        public abstract void Use(State.Game game);
    }
}
