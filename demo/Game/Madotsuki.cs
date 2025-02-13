using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Game.Graphics;

namespace demo.Game
{
    internal class Madotsuki
    {
        public static Texture2D madoSpriteSheet;
        public static Microsoft.Xna.Framework.Rectangle mdBL = new Microsoft.Xna.Framework.Rectangle(12, 20, 15, 28);
        public static Microsoft.Xna.Framework.Rectangle mdBN = new Microsoft.Xna.Framework.Rectangle(34, 20, 15, 28);
        public static Microsoft.Xna.Framework.Rectangle mdBR = new Microsoft.Xna.Framework.Rectangle(59, 20, 15, 28);
        public static Microsoft.Xna.Framework.Rectangle mdRL = new Microsoft.Xna.Framework.Rectangle(12, 52, 15, 31);
        public static Microsoft.Xna.Framework.Rectangle mdRN = new Microsoft.Xna.Framework.Rectangle(35, 52, 21, 31);
        public static Microsoft.Xna.Framework.Rectangle mdRR = new Microsoft.Xna.Framework.Rectangle(60, 52, 16, 31);
        public static Microsoft.Xna.Framework.Rectangle mdFL = new Microsoft.Xna.Framework.Rectangle(12, 84, 15, 29);
        public static Microsoft.Xna.Framework.Rectangle mdFN = new Microsoft.Xna.Framework.Rectangle(36, 84, 16, 29);
        public static Microsoft.Xna.Framework.Rectangle mdFR = new Microsoft.Xna.Framework.Rectangle(60, 84, 15, 29);
        public static Microsoft.Xna.Framework.Rectangle mdLL = new Microsoft.Xna.Framework.Rectangle(12, 116, 14, 29);
        public static Microsoft.Xna.Framework.Rectangle mdLN = new Microsoft.Xna.Framework.Rectangle(36, 116, 16, 29);
        public static Microsoft.Xna.Framework.Rectangle mdLR = new Microsoft.Xna.Framework.Rectangle(60, 116, 15, 29);
        public static Microsoft.Xna.Framework.Rectangle mdKnifeB = new Microsoft.Xna.Framework.Rectangle(36, 309, 16, 28);
        public static Microsoft.Xna.Framework.Rectangle mdKnifeF = new Microsoft.Xna.Framework.Rectangle(254, 374, 17, 27);
        public static Microsoft.Xna.Framework.Rectangle mdDamaged = new Microsoft.Xna.Framework.Rectangle(483, 527, 21, 17);
        public static Microsoft.Xna.Framework.Rectangle mdKnifeL = new Microsoft.Xna.Framework.Rectangle(249, 404, 21, 29);
        public static Microsoft.Xna.Framework.Rectangle mdKnifeR = new Microsoft.Xna.Framework.Rectangle(309, 341, 21, 18);
        public static Microsoft.Xna.Framework.Rectangle[] mdBack = new Microsoft.Xna.Framework.Rectangle[] { mdBN, mdBL, mdBN, mdBR };
        public static Microsoft.Xna.Framework.Rectangle[] mdRight = new Microsoft.Xna.Framework.Rectangle[] { mdRN, mdRL, mdRN, mdRR };
        public static Microsoft.Xna.Framework.Rectangle[] mdFront = new Microsoft.Xna.Framework.Rectangle[] { mdFN, mdFL, mdFN, mdFR };
        public static Microsoft.Xna.Framework.Rectangle[] mdLeft = new Microsoft.Xna.Framework.Rectangle[] { mdLN, mdLL, mdLN, mdLR };
        public static void LoadMadotsuki(Player player)
        {
            player.animationSequences = new Dictionary<Player.srcSprites, Animation>();
            player.animationSequences.Add(Player.srcSprites.UP, new Animation(madoSpriteSheet, mdBack));
            player.animationSequences.Add(Player.srcSprites.DOWN, new Animation(madoSpriteSheet, mdFront));
            player.animationSequences.Add(Player.srcSprites.RIGHT, new Animation(madoSpriteSheet, mdRight));
            player.animationSequences.Add(Player.srcSprites.LEFT, new Animation(madoSpriteSheet, mdLeft));
            player.animationSequences.Add(Player.srcSprites.ATTACK, new Animation(madoSpriteSheet, new Microsoft.Xna.Framework.Rectangle[] { mdKnifeF}));
            player.animationSequences.Add(Player.srcSprites.DAMAGED, new Animation(madoSpriteSheet, new Microsoft.Xna.Framework.Rectangle[] { mdDamaged}));
        }
    }
}
