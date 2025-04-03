using System.Dynamic;
using Game.Entities;

namespace Game.Commands;
public class DamageCommand : ICommand {
    private int damage;
    private Player player;
    public DamageCommand(Player player, int damage) {
        this.player = player;
        this.damage = damage;
    }
    public void Execute() {
        player.inflict(damage);

    }
}