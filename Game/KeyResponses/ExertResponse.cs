using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Util;
namespace KeyResponses;
public class ExertResponse : IKeyResponse{
    Player player;
    public ExertResponse(Player c){
        this.player = c;
    }
    public Game.State.Game game;

    public Vector2 respond(){
        if(player.Item != null){
            player.Item.Use(game);
            if (this.player.ActiveAnimation != Player.ATTACK)
					{
						game.sfx["punch"].Play();
						TempBuffer.add(new TempEntity(TempBuffer.pow, this.player.Position), 1000);
						int padding = 30;
						// Widespread area hitbox for testing. Later we want this to be directional
						game.room.AddHitbox(new(10, this.player, new(-padding, -padding, this.player.collisionBox.Width + padding * 2, this.player.collisionBox.Height + padding * 2)));
					}
					this.player.ActiveAnimation = Player.ATTACK;
        }
        return new Vector2(0, 0);
    }
}