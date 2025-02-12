using Game.Entities;

namespace Game.Items
{
    public interface IItem : IGameObject
    {
        /// Item was used
        public abstract void Use();
    }
}
