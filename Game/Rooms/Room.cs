using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Game.Entities;
using Game.Tiles;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Rooms
{
    public class Room
    {
        // Every room has the same outline (for now)
        private static readonly Texture2D OUTLINE = Main.Load("Tiles/map_outline.png");

        // Every room in Zelda is 12x7 (we secretely expand this to 14x9 for collision logic)
        public TileType[] tiles;
        public Door[] doors;
        public List<Entity> gameObjects = [];

        // public List<Noxa> noxe = new List<Noxa>();

        // Needed for serialization
        private Room() { }

        /// True if tile is on the border of the map, false otherwise
        private static bool IsBorderTile(int index) => index % 14 == 0 || index % 14 == 13 || index / 14 == 0 || index / 14 == 8;

        public virtual void Update(State.Game game)
        {
            // Not doing any fancy lambda because that would be concurrent modification
            // This apparently works??
            foreach (var entity in gameObjects.ToList())
            {
                entity.Update(game);

                // Special interactions with LivingEntity
                if (entity is LivingEntity cast)
                {
                    Vector2 velocity = cast.Move(game);
                    HandleAxisCollision(cast, velocity.X, true);
                    HandleAxisCollision(cast, velocity.Y, false);
                }
            }
            // damage();
            // noxe = new List<Noxa>();
            // Tick doors (check for intersection)
            foreach (var door in doors)
            {
                door.Update(game);
            }
        }

        // private void damage(){
        //     for(int i = 0; i < noxe.Legnth; i++){
        //         for(int a = 0; a < gameObjects.Length; a++){
        //             if(gameObjects[a] is LivingEntity le){
        //              if(le != noxe[i].originator){
        //                     if(Math.pow(le.location.x - noxe[i].loc.x, 2) + Math.pow(le.location.Y -noxe[i].loc.y, 2) < Math.pow(noxe[i].radius, 2)){
        //                         le.inflict(noxe[i].damage);
        //                    }
        //                }
        //             }
        //         }
        //     }
        // }

        private void HandleAxisCollision(LivingEntity entity, float velocity, bool xAxis)
        {
            // For each axis, step until we collide with something
            // Move across X axis
            bool positiveMovement = velocity > 0;
            float distance = Math.Abs(velocity);

            while (distance > 0)
            {
                // Initialize with minimum step size
                float step = Math.Min((xAxis ? entity.collisionBox.Width : entity.collisionBox.Height) / 2f, distance);

                // Move player the step distance to check if we can actually move there
                if (xAxis)
                {
                    entity.Position += new Vector2(step * (positiveMovement ? 1 : -1), 0);
                }
                else
                {
                    entity.Position += new Vector2(0, step * (positiveMovement ? 1 : -1));
                }

                bool contacted = false;
                int snapPoint = 0;

                // Test if we intersect with any wall tile
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (Tile.IsWalkable(tiles[i])) continue;
                    int tileX = Tile.TILE_SIZE + i % 14 * Tile.TILE_SIZE;
                    int tileY = Tile.TILE_SIZE + i / 14 * Tile.TILE_SIZE;
                    if (entity.collisionBox.Intersects(new Rectangle(tileX - (int)entity.Position.X, tileY - (int)entity.Position.Y, Tile.TILE_SIZE, Tile.TILE_SIZE)))
                    {
                        int newSnap = positiveMovement ? (xAxis ? tileX - entity.collisionBox.Width - entity.collisionBox.X : tileY - entity.collisionBox.Height - entity.collisionBox.Y) : (xAxis ? tileX - entity.collisionBox.X : tileY - entity.collisionBox.Y) + Tile.TILE_SIZE;

                        if (!contacted || (positiveMovement && newSnap < snapPoint) || (!positiveMovement && newSnap > snapPoint))
                        {
                            snapPoint = newSnap;
                            contacted = true;
                        }
                    }
                }

                // If we collided with any blocks
                if (contacted)
                {
                    // Snap entity to contact point
                    // Add some push away from the wall so stuttering effect doesn't happen. This is a quirk of the Intersects logic
                    if (xAxis)
                    {
                        entity.Position.X = snapPoint;
                    }
                    else
                    {
                        entity.Position.Y = snapPoint;
                    }
                    return;
                }

                // Process step
                distance -= step;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw room outline
            batch.Draw(OUTLINE, new Vector2(0, 0), Color.White);

            for (int i = 0; i < tiles.Length; i++)
            {
                // Do not draw border tiles
                if (IsBorderTile(i)) continue;
                Tile.Draw(tiles[i], new Vector2(Tile.TILE_SIZE + i % 14 * Tile.TILE_SIZE, Tile.TILE_SIZE + i / 14 * Tile.TILE_SIZE), batch);
            }

            // Draw doors with rotation
            foreach (var door in doors)
            {
                door.Draw(batch);
            }

            // Draw entities over tiles
            gameObjects.ForEach(entity => entity.Draw(batch));
        }

        public void AddEntity(Entity entity) => gameObjects.Add(entity);
        public void RemoveEntity(Entity entity) => gameObjects.Remove(entity);

        public static Room LoadRoom(string filename)
        {
            XmlSerializer serializer = new(typeof(Room));
            using Stream reader = new FileStream("Content/Rooms/" + filename + ".xml", FileMode.Open);
            Room room = (Room)serializer.Deserialize(reader);
            // Add room boundaries
            TileType[] trueTiles = new TileType[14 * 9];
            for (int i = 0, innerIndex = 0; i < trueTiles.Length; i++)
            {
                // If on the outskirts, put an invisible wall there
                if (IsBorderTile(i))
                {
                    // Top door
                    if (i >= 6 && i <= 7)
                    {
                        trueTiles[i] = Door.IsWalkable(room.doors[0].Type) ? TileType.BLOCK : TileType.WALL;
                        if (room.doors[0].Type == DoorType.LOCK && Player.Key >= 1)
                        {
                            trueTiles[i] = TileType.BLOCK;
                        }
                    }
                    else if (i == 56)
                    {
                        // Left door
                        trueTiles[i] = Door.IsWalkable(room.doors[1].Type) ? TileType.BLOCK : TileType.WALL;
                        if (room.doors[1].Type == DoorType.LOCK && Player.Key >= 1)
                        {
                            trueTiles[i] = TileType.BLOCK;
                        }
                    }
                    else if (i == 69)
                    {
                        // Right door
                        trueTiles[i] = Door.IsWalkable(room.doors[3].Type) ? TileType.BLOCK : TileType.WALL;
                        if (room.doors[3].Type == DoorType.LOCK && Player.Key >= 1)
                        {
                            trueTiles[i] = TileType.BLOCK;
                        }
                    }
                    else if (i >= 118 && i <= 119)
                    {
                        // Bottom door
                        trueTiles[i] = Door.IsWalkable(room.doors[2].Type) ? TileType.BLOCK : TileType.WALL;
                        if (room.doors[2].Type == DoorType.LOCK && Player.Key >= 1)
                        {
                            trueTiles[i] = TileType.BLOCK;
                        }
                    }
                    else
                    {
                        trueTiles[i] = TileType.WALL;
                    }
                }
                else
                {
                    trueTiles[i] = room.tiles[innerIndex++];
                }
            }
            room.tiles = trueTiles;
            // Initialize doors
            foreach (var door in room.doors)
            {
                door.Initialize();
            }
            return room;
        }
    }
}
