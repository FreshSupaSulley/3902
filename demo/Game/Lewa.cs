using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace demo.Game
{
    internal class Lewa
    {
        public static Texture2D texture;
        //automating this because I cannot manually map out all these sprites (sorry Dr. Boggus, please only look at Monoko)
        static int spriteHeight = 64;
        static int spriteWidth = 64;
        public static Microsoft.Xna.Framework.Rectangle[] lwF = Lw_populate(420);
        public static Microsoft.Xna.Framework.Rectangle[] lwB = Lw_populate(20);
        public static Microsoft.Xna.Framework.Rectangle[] lwR = Lw_populate(220);
        public static Microsoft.Xna.Framework.Rectangle[] lwL = Lw_populate(620);
        public static Microsoft.Xna.Framework.Rectangle[][] lwAttack = new Microsoft.Xna.Framework.Rectangle[][] { new Microsoft.Xna.Framework.Rectangle[] { new Microsoft.Xna.Framework.Rectangle(1220, 406, 64, 82) }, new Microsoft.Xna.Framework.Rectangle[] { new Microsoft.Xna.Framework.Rectangle(1220, 0, 64, 90) }, new Microsoft.Xna.Framework.Rectangle[] { new Microsoft.Xna.Framework.Rectangle(1220, 600, 64, 90) }, new Microsoft.Xna.Framework.Rectangle[] { new Microsoft.Xna.Framework.Rectangle(1220, 200, 64, 90) } };
        public static Microsoft.Xna.Framework.Rectangle lwDamaged = new Microsoft.Xna.Framework.Rectangle(0, 424, 70, 65);

        public static Microsoft.Xna.Framework.Rectangle[] Lw_populate(int y)
        {
            Microsoft.Xna.Framework.Rectangle[] temp = new Microsoft.Xna.Framework.Rectangle[10];
            for (int i = 0; i < 10; i++)
            {
                temp[i] = new Microsoft.Xna.Framework.Rectangle(100 * (i+1), y, spriteHeight, spriteWidth);
            }
            return temp;
        }
    }
}
