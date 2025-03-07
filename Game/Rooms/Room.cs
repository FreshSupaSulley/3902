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
        private Door topDoor, rightDoor, bottomDoor, leftDoor;
        private readonly DoorType topType, rightType ,bottomType, leftType;
        private readonly String leftName, rightName;
        public readonly List<Entity> gameObjects = [];
        Game game;

        public Room(Game game, Player player, TileType[] tiles, DoorType topDoor, DoorType rightDoor, DoorType bottomDoor, DoorType leftDoor, String leftRoom, String rightRoom)
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
                        // Bottom door
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
                this.rightName = rightRoom;
                this.leftName = leftRoom;
                this.game = game;

                leftType = leftDoor;
                topType = topDoor;
                rightType = rightDoor;
                bottomType = bottomDoor;
            }
            // Add new tiles
        }
        private bool loaded = false;
        public void PostLoad() {
            this.leftDoor = new Door(leftType, 0, game.rooms[leftName], game, game.player);
            this.topDoor = new Door(topType, 1, null, game, game.player);
            this.rightDoor = new Door(rightType, 2, game.rooms[rightName], game, game.player);
            this.bottomDoor = new Door(bottomType, 3, null, game, game.player);

            loaded = true;
        }

        /// True if tile is on the border of the map, false otherwise
        private static bool IsBorderTile(int index) => index % 14 == 0 || index % 14 == 13 || index / 14 == 0 || index / 14 == 8;

        public virtual void Update(Game game)
        {
            if (!loaded) PostLoad();
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
                    entity.Position += new Vector2(step * (positiveMovement ? 1 : -1),0);
                }
                else
                {
                    entity.Position += new Vector2(0,step * (positiveMovement ? 1 : -1));
                }

                bool contacted = false;
                int snapPoint = 0;

                // Test if we intersect with any wall tile
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (Tile.IsWalkable(tiles[i])) continue;
                    int tileX = Tile.TILE_SIZE + i % 14 * Tile.TILE_SIZE;
                    int tileY = Tile.TILE_SIZE + i / 14 * Tile.TILE_SIZE;
                    if (entity.collisionBox.bounds.Intersects(new Rectangle(tileX, tileY, Tile.TILE_SIZE, Tile.TILE_SIZE)))
                    {
                        Console.WriteLine("=================");
                        Console.WriteLine(entity.collisionBox.bounds);
                        Console.WriteLine(new Rectangle(tileX, tileY, Tile.TILE_SIZE, Tile.TILE_SIZE));
                        Console.WriteLine(IsBorderTile(i));
                        Console.WriteLine(i);
                        Console.WriteLine("=================");
                        int newSnap;
                        if (positiveMovement) {
                            newSnap = (xAxis ? 
                                tileX - entity.collisionBox.bounds.Width
                                :
                                tileY - entity.collisionBox.bounds.Height
                            );
                        } else {
                            newSnap = (xAxis ? 
                                tileX + Tile.TILE_SIZE
                                : 
                                tileY+ Tile.TILE_SIZE
                            );
                        }
                        Console.WriteLine(newSnap);
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
                        entity.Position = new Vector2(snapPoint, entity.Position.Y);// + (positiveMovement ? 0.1f : -0.1f);
                    }
                    else
                    {
                        entity.Position = new Vector2(entity.Position.X, snapPoint);// + (positiveMovement ? 0.1f : -0.1f);
                    }
                    return;
                }

                // Process step
                distance -= step;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (!loaded) PostLoad();
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
