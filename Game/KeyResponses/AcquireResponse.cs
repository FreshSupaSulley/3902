using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Items;

namespace Game.KeyResponses{
public class AcquireResponse : IKeyResponse{
    Player player;
    Item i;
    public static int speed = 1;
    public AcquireResponse(Player c, Item item){
        this.player = c;
        this.i = item;
    }

    public Vector2 respond(){
        player.Item = i;
        return new Vector2(0, 0);
    }
    public void processGame(Game.State.Game game){
        
    }
}
}