using Microsoft.Xna.Framework;
namespace Game.KeyResponses;
public interface IKeyResponse{
    Vector2 respond();
    void processGame(Game.State.Game game);
}