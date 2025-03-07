using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Game.Collision;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Rooms
{
    [XmlRoot("Room")]
    public abstract class Room
    {
        // Every room has the same outline (for now)
        private static readonly Texture2D OUTLINE = Game.Load("Tiles/map_outline.png");

        // Every room in Zelda is 12x7 (we secretely expand this to 14x9 for collision logic)
        [XmlArray("Room tiles")]
        public TileType[] tiles;
        private readonly Door topDoor, rightDoor, bottomDoor, leftDoor;
        public readonly List<Entity> gameObjects = [];

        public Room(Game game, Player player, TileType[] tiles, DoorType topDoor, DoorType rightDoor, DoorType bottomDoor, DoorType leftDoor, Room leftRoom, Room rightRoom)
        {
            gameObjects.Add(player);
            // Add room boundaries
            this.tiles = new TileType[14 * 9];
            for (int i = 0, innerIndex = 0; i < this.tiles.Length; i++)
            {
                // If on the outskirts, put a wall there
                if (IsBorderTile(i))
                {
                    int[] doorIndices = [6, 7, 56, 69, 118, 119];
                    // Top door
                    if (i >= 6 && i <= 7)
                    {
                        this.tiles[i] = Door.IsWalkable(topDoor) ? TileType.BLOCK : TileType.WALL;
                    }
                    else if (i == 56)
                    {
                        // Left door
                        this.tiles[i] = Door.IsWalkable(leftDoor) ? TileType.BLOCK : TileType.WALL;
                    }
                    else if (i == 69)
                    {
                        // Right door
                        this.tiles[i] = Door.IsWalkable(rightDoor) ? TileType.BLOCK : TileType.WALL;
                    }
                    else if (i >= 118 && i <= 119)
                    {
                        // Right door
                        this.tiles[i] = Door.IsWalkable(bottomDoor) ? TileType.BLOCK : TileType.WALL;
                    }
                    else
                    {
                        this.tiles[i] = TileType.WALL;
                    }
                }
                else
                {
                    this.tiles[i] = tiles[innerIndex++];
                }
            }
            // Add new tiles
            this.topDoor = new Door(topDoor, 1, null, game);
            this.rightDoor = new Door(rightDoor, 2, rightRoom, game);
            this.bottomDoor = new Door(bottomDoor, 3, null, game);
            this.leftDoor = new Door(leftDoor, 0, leftRoom, game);
        }

        /// True if tile is on the border of the map, false otherwise
        private static bool IsBorderTile(int index) => index % 14 == 0 || index % 14 == 13 || index / 14 == 0 || index / 14 == 8;

        public virtual void Update(Game game)
        {
            // Not doing any fancy lambda because that would be concurrent modification
            // This apparently works??
            topDoor.Update();
            bottomDoor.Update();
            leftDoor.Update();
            rightDoor.Update();

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
        }

        private void HandleAxisCollision(LivingEntity entity, float velocity, bool xAxis)
        {
            // For each axis, step until we collide with something
            // Move across X axis
            bool positiveMovement = velocity > 0;
            float distance = Math.Abs(velocity);

            while (distance > 0)
            {
                // Initialize with minimum step size
                float step = Math.Min((xAxis ? entity.collisionBox.bounds.Width : entity.collisionBox.bounds.Height) / 2f, distance);

                // Move player the step distance to check if we can actually move there
                if (xAxis)
                {
                    entity.Position.X += step * (positiveMovement ? 1 : -1);
                }
                else
                {
                    entity.Position.Y += step * (positiveMovement ? 1 : -1);
                }

                bool contacted = false;
                int snapPoint = 0;

                // Test if we intersect with any wall tile
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (Tile.IsWalkable(tiles[i])) continue;
                    int tileX = Tile.TILE_SIZE + i % 14 * Tile.TILE_SIZE, tileY = Tile.TILE_SIZE + i / 14 * Tile.TILE_SIZE;
                    if (entity.collisionBox.bounds.Intersects(new Rectangle(tileX - (int)entity.Position.X, tileY - (int)entity.Position.Y, Tile.TILE_SIZE, Tile.TILE_SIZE)))
                    {
                        int newSnap = positiveMovement ? (xAxis ? tileX - entity.collisionBox.bounds.Width - entity.collisionBox.bounds.X : tileY - entity.collisionBox.bounds.Height - entity.collisionBox.bounds.Y) : (xAxis ? tileX - entity.collisionBox.bounds.X : tileY - entity.collisionBox.bounds.Y) + Tile.TILE_SIZE;

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
                        entity.Position.X = snapPoint;// + (positiveMovement ? 0.1f : -0.1f);
                    }
                    else
                    {
                        entity.Position.Y = snapPoint;// + (positiveMovement ? 0.1f : -0.1f);
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
            topDoor.Draw(batch, 0, new Vector2(112, 0));
            rightDoor.Draw(batch, 90, new Vector2(224, 72));
            bottomDoor.Draw(batch, 180, new Vector2(112, 144));
            leftDoor.Draw(batch, 270, new Vector2(0, 72));

            // Draw entities over tiles
            gameObjects.ForEach(entity => entity.Draw(batch));
        }

        public void AddEntity(Entity entity) => gameObjects.Add(entity);
        public void RemoveEntity(Entity entity) => gameObjects.Remove(entity);
    }
}
