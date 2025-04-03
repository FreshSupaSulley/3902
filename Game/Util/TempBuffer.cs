using System.Collections.Generic;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Util
{
    internal class TempBuffer
    {
        public static Texture2D pow { get; set; }
        public static int powLength = 50;
        public static int powHeight = 50;
        //maps temporary entity to time it should be eliminated
        public static Dictionary<int, TempEntity> current = new Dictionary<int, TempEntity>();
        public static int elapsed = 0;
        public static List<int> expiries = new List<int>();
        public static void add(TempEntity t, int duration)
        {
            if (!current.ContainsKey(elapsed + duration))
            {
                current.Add(elapsed + duration, t);
                expiries.Add(elapsed + duration);
            }
            else
            {
                int i = 1;
                while (current.ContainsKey(elapsed + duration + i))
                {
                    i++;
                }
                current.Add(elapsed + duration + i, t);
                expiries.Add(elapsed + duration + i);
            }
        }
        public static void depreciate(Main game)
        {
            foreach (int key in expiries)
            {
                current[key].Update(null);
            }
            if (current.ContainsKey(elapsed))
            {
                current.Remove(elapsed);
                expiries.Remove(elapsed);
            }
        }
    }
}
