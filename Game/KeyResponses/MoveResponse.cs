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
            this.player.ActiveAnimation = this.player.ownLeft;
        }
        if(hdsp > 0){
            this.player.ActiveAnimation = this.player.ownRight;
        }
        if(vdsp < 0){
            this.player.ActiveAnimation = this.player.ownUp;
        }
        if(vdsp > 0){
            this.player.ActiveAnimation = this.player.ownDown;
        }
        return new Vector2(hdsp, vdsp);
    }
    public void processGame(Game.State.Game game){

    }
}
}