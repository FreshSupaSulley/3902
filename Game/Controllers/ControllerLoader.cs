using System.Collections.Generic;
using Game.Commands;
using Microsoft.Xna.Framework.Input;
using Game.Items;
using Game.Entities;
using Game.Tiles;

namespace Game.Controllers
{
    class ControllerLoader
    {
        // public static void LoadSprint2Commands(Tile tile, Game game, KeyboardController keyboard, Player p, MobileEntity[] entities, List<IGameObject> gameObjects)
        // {
        //     Dictionary<Keys, ICommand> m = new Dictionary<Keys, ICommand>();
        //     m.Add(Keys.Up, new PlayerMovementCommand(p, -1, 1));
        //     m.Add(Keys.Down, new PlayerMovementCommand(p, 1, 1));
        //     m.Add(Keys.Right, new PlayerMovementCommand(p, 1, 0));
        //     m.Add(Keys.Left, new PlayerMovementCommand(p, -1, 0));
        //     m.Add(Keys.W, new PlayerMovementCommand(p, -1, 1));
        //     m.Add(Keys.S, new PlayerMovementCommand(p, 1, 1));
        //     m.Add(Keys.D, new PlayerMovementCommand(p, 1, 0));
        //     m.Add(Keys.A, new PlayerMovementCommand(p, -1, 0));
        //     m.Add(Keys.N, new PlayerAttackCommand(game, p));
        //     m.Add(Keys.Z, new PlayerAttackCommand(game, p));
        //     m.Add(Keys.E, new PlayerDamageCommand(p));
        //     // // m.Add(Keys.Y, new PlayerSwitchCommand(p, Monoko.monoko, Monoko.mkBack, Monoko.mkFront, Monoko.mkLeft, Monoko.mkRight, new Rectangle[] { Monoko.scaryDefault }, new Rectangle[] { Monoko.mkEmotionallyDamaged }));
        //     // // m.Add(Keys.U, new PlayerSwitchCommand(p, Madotsuki.madoSpriteSheet, Madotsuki.mdBack, Madotsuki.mdFront, Madotsuki.mdLeft, Madotsuki.mdRight, new Rectangle[] { Madotsuki.mdKnifeFRetract, Madotsuki.mdKnifeF }, new Rectangle[] { Madotsuki.mdDamaged }, new Rectangle[][] { new Rectangle[] { Madotsuki.mdKnifeF}, new Rectangle[] { Madotsuki.mdKnifeB }, new Rectangle[] {Madotsuki.mdKnifeL }, new Rectangle[] { Madotsuki.mdKnifeR} }));
        //     // // m.Add(Keys.I, new PlayerSwitchCommand(p, Lewa.texture, Lewa.lwB, Lewa.lwF, Lewa.lwL, Lewa.lwR, new Rectangle[] { Lewa.lwAttack[0][0] }, new Rectangle[] { Lewa.lwDamaged }, Lewa.lwAttack));
        //     m.Add(Keys.O, new EnemySwitchCommand(0, entities, gameObjects));
        //     m.Add(Keys.P, new EnemySwitchCommand(1, entities, gameObjects));
        //     // Player items
        //     m.Add(Keys.D1, new SwitchItemCommand(p, () => new Heart()));
        //     m.Add(Keys.D2, new SwitchItemCommand(p, () => new Boomerang()));
        //     m.Add(Keys.D3, new SwitchItemCommand(p, () => new Bomb()));
        //     // m.Add(Keys.D2, new SwitchItemCommand(p, new Boomerang()));
        //     // m.Add(Keys.D1, new SwitchItemCommand(p, new Bomb()));
        //     // m.Add(Keys.P, new EnemySwitchCommand(1));
        //     keyboard.AddCommand(m);
        //     // Switch tile
        //     Dictionary<Keys, ICommand> m2 = new Dictionary<Keys, ICommand>();
        //     keyboard.AddKeydownCommand(Keys.T, new SwitchTileCommand(tile, true));
        //     keyboard.AddKeydownCommand(Keys.Y, new SwitchTileCommand(tile, false));
        //     // Switch item
        //     keyboard.AddKeydownCommand(Keys.U, new ReplaceItemCommand(game, true));
        //     keyboard.AddKeydownCommand(Keys.I, new ReplaceItemCommand(game, false));
        // }
    }
}
