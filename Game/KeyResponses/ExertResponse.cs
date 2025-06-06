using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Util;
using Game.Items;
namespace Game.KeyResponses;
public class ExertResponse : IKeyResponse{
    Player player;
    public ExertResponse(Player player){
        this.player = player;
    }
    public Game.State.Game game;

    public Vector2 respond(){
        if(player.Item != null) {
            player.Item.Use(game);
            player.Item = null;
        }
        if (this.player.ActiveAnimation != this.player.ownAttack)
        {
            game.sfx["punch"].Play();
            TempBuffer.add(new TempEntity(TempBuffer.pow, this.player.Position), 1000);
            int padding = 30;
            // Widespread area hitbox for testing. Later we want this to be directional
            game.room.AddHitbox(new(10, this.player, new(-padding, -padding, this.player.collisionBox.Width + padding * 2, this.player.collisionBox.Height + padding * 2)));
        }
        this.player.ActiveAnimation = this.player.ownAttack;
        return new Vector2(0, 0);
    }
    public void processGame(Game.State.Game game){
        this.game = game;
    }
}