using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Commands;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace demo.Game.Commands
{
    internal class PlayerSwitchCommand : PlayerCommand
    {
        Texture2D texture;
        Microsoft.Xna.Framework.Rectangle[] b;
        Microsoft.Xna.Framework.Rectangle[] f;
        Microsoft.Xna.Framework.Rectangle[] l;
        Microsoft.Xna.Framework.Rectangle[] r;
        Microsoft.Xna.Framework.Rectangle[] a;
        Microsoft.Xna.Framework.Rectangle[] d;
        public PlayerSwitchCommand(Player p, Texture2D txt, Microsoft.Xna.Framework.Rectangle[] back, Microsoft.Xna.Framework.Rectangle[] front, Microsoft.Xna.Framework.Rectangle[] left, Microsoft.Xna.Framework.Rectangle[] right, Microsoft.Xna.Framework.Rectangle[] attack, Microsoft.Xna.Framework.Rectangle[] damaged) : base(p)
        {
            texture = txt;
            b = back;
            f = front;
            l = left;
            r = right;
            a = attack;
            d = damaged;
        }
        public override void Execute()
        {
            player.animationSequences = new Dictionary<Player.srcSprites, Animation>();
            player.animationSequences.Add(Player.srcSprites.UP, new Animation(texture, b));
            player.animationSequences.Add(Player.srcSprites.DOWN, new Animation(texture, f));
            player.animationSequences.Add(Player.srcSprites.RIGHT, new Animation(texture, r));
            player.animationSequences.Add(Player.srcSprites.LEFT, new Animation(texture, l));
            player.animationSequences.Add(Player.srcSprites.ATTACK, new Animation(texture, a));
            player.animationSequences.Add(Player.srcSprites.DAMAGED, new Animation(texture, d));
            player.Update();
  
        }
    }
}
