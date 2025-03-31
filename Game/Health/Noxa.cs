using System;
using Game.Entities;

public class Noxa{
    public Entity originator;
    public int damage;
    public int radius;
    public Vector2 loc;
    public Noxa(Entity o, int d, int r, Vector2 location){
        originator = o;
        damage = d;
        radius = r;
        loc = location;
    }
}