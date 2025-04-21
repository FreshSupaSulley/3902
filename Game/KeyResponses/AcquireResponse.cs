using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Items;
using System.Reflection;
using System;
using System.Globalization;
using Microsoft.Xna.Framework.Content;

namespace Game.KeyResponses{
    public class AcquireResponse : IKeyResponse{
        private Player player;
        public static int speed = 1;
        private string itemName;
        public AcquireResponse(Player player, string itemName){
            this.itemName = itemName;
            this.player = player;
        }

        public Vector2 respond(){
            switch (itemName) {
                case "banana":
                    player.Item = new Banana(player.Position, player);
                    break;
                case "heart":
                    player.Item = new Heart(player.Position, player);
                    break;
            }
            return new Vector2(0, 0);
        }
        public void processGame(Game.State.Game game){}
    }
}