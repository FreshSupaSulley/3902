using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics
{
    public static class AnimationRegistry
    {
        // Static fields for predefined animations
        public static Animation Run;
        public static Animation Jump;
        public static Animation Idle;

        // Static Load method to initialize all animations
        public static void Load(GraphicsDevice device)
        {
            // Load each animation
            Run = new Animation(Load(device, "run.png"), 3, 3);
        }

        private static Texture2D Load(GraphicsDevice device, string path)
        {
            // Try with resources
            using (var fileStream = new FileStream("Content/Sprites/" + path, FileMode.Open))
            {
                return Texture2D.FromStream(device, fileStream);
            }
        }
    }
}
