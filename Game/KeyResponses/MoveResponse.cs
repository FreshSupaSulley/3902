using Game.Entities;
using Microsoft.Xna.Framework;

namespace Game.KeyResponses{
public class MoveResponse : IKeyResponse{
    Player player;
    int hdsp;
    int vdsp;
    public MoveResponse(Player c, int horizontalDisplacement, int verticalDisplacement){
        this.player = c;
        this.hdsp = horizontalDisplacement;
        this.vdsp = verticalDisplacement;
    }

    public Vector2 respond(){
        if(hdsp < 0){
            this.player.ActiveAnimation = Player.LEFT;
        }
        if(hdsp > 0){
            this.player.ActiveAnimation = Player.RIGHT;
        }
        if(vdsp < 0){
            this.player.ActiveAnimation = Player.UP;
        }
        if(vdsp > 0){
            this.player.ActiveAnimation = Player.DOWN;
        }
        return new Vector2(hdsp, vdsp);
    }
    public void processGame(Game.State.Game game){

    }
}
}