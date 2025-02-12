using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo.Game.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace demo.Game
{
    internal class TempBuffer
    {
        public static Texture2D pow { get; set; } 
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
        }
        public static void depreciate()
        {
            foreach(int key in expiries)
            {
                current[key].Update();
            }
            if (current.ContainsKey(elapsed))
            {
                current.Remove(elapsed);
                expiries.Remove(elapsed);
            }
        }
    }
}
