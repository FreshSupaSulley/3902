using Microsoft.Xna.Framework.Graphics;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Tiles;
using Game.Rooms;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Game.Util;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Game.State
{
    public class Game : IGameState
    {
        // Used to load resources statically
        private RenderTarget2D target, loadingTarget;

        // Visible to inheritors
        //public Player player;
        public List<Player> players = new List<Player>();
        public int playerCount = 2;
        public Room room;
        public KeyboardController keyboard;
        public MouseController mouse;

        // Pertains to loading rooms
        private readonly int TRANSITION_TIME = 60;
        private bool renderCaptured;
        private int loadingTime, loadingDirection;
        private Room loadingRoom;

        //sound effects to be played once (i.e. without looping)
        public Dictionary<String, SoundEffect> sfx = [];
        //currently playing music
        public SoundEffectInstance bgm = null;

        public int muteBit = 0;
        public int muteRequest = 0;

        // Load static assets
        static Game()
        {
            Tile.LoadTextures();
            Door.LoadTextures();
            TempBuffer.pow = Main.Load("pow.png");
        }

        public Game()
        {
            // Size of Zelda map
            target = new RenderTarget2D(Main.device, (12 + 4) * 16, (10 + 4) * 16);
            loadingTarget = new RenderTarget2D(Main.device, target.Width, target.Height);
            // Load start room. This also defines the player
            for(int i = 0; i < playerCount; i++){
                Player p = new Player();
                players.Add(p);
            }
            //this.player = new Player();
            room = Room.LoadRoom("start", this.players);
            players[0] = (Player)room.gameObjects.Find(entity => entity is Player);
            players[0].makeMappings(Player.left_map);
            players[playerCount - 1] = (Player)room.gameObjects.FindLast(entity => entity is Player);
            players[playerCount - 1].makeMappings(Player.right_map);
            LoadSoundEffect("ding.wav");
            LoadSoundEffect("punch.wav");
            LoadSoundEffect("fart.wav");
            LoadSoundEffect("wow.wav");
            LoadSoundEffect("pain.wav");
            ChangeMusic("Song_1.wav");
        }

        /// Called when the game state is switched to something else
        public void OnExit()
        {
            bgm.Stop();
        }

        public void Update(GameTime gameTime)
        {
            // If we want to switch to pause
            if (Main.INSTANCE.keyboard.IsKeyPressed(Keys.Escape))
            {
                Main.uiManager.SetGame(this);
                Main.SwitchGameState(new Pause(this));
                Main.uiManager.ChangeUIState("pause");
                return;
            }

            // Tick room if not mid-transition
            if (loadingRoom is null)
            {
                room.Update(this);
                InGameMessage.agit(this);
            }
            // If we are switching between rooms
            else
            {
                if (loadingTime++ >= TRANSITION_TIME)
                {
                    room = loadingRoom;
                    loadingRoom = null;
                    loadingTime = 0;
                    renderCaptured = false;
                }
            }

            if (muteRequest == 1)
            {
                if (muteBit == 0)
                {
                    this.Mute();
                }
                else
                {
                    this.Unmute();
                }
                muteRequest = 0;
            }
        }

        // Render
        public void Draw(SpriteBatch spriteBatch)
        {
            // If loading
            if (loadingRoom is null)
            {
                // Draw room at scaled resolution
                Main.device.SetRenderTarget(target);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
                room.Draw(spriteBatch);
                InGameMessage.drawAll(spriteBatch);
                spriteBatch.End();
            }
            else if (renderCaptured is false)
            {
                // Put player in correct location
                foreach(Player player in players){
                  switch (loadingDirection)
                  {
                    // Top
                    case 0:
                        player.ActiveAnimation = player.ownDown;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 32 - player.collisionBox.Y);
                        break;
                    // Bottom
                    case 2:
                        player.ActiveAnimation = player.ownUp;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 144 - player.collisionBox.Y - player.collisionBox.Height);
                        break;
                    // Left
                    case 3:
                        player.ActiveAnimation = player.ownRight;
                        player.Position = new(32 - player.collisionBox.X, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;
                    // Right
                    case 1:
                        player.ActiveAnimation = player.ownLeft;
                        player.Position = new(224 - player.collisionBox.X - player.collisionBox.Width, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;

                    }
                }
                renderCaptured = true;
                Main.device.SetRenderTarget(loadingTarget);
                spriteBatch.Begin();
                loadingRoom.Draw(spriteBatch);
                spriteBatch.End();
            }

            // Number of pixels
            double sin = Math.Pow(Math.Sin(loadingTime * Math.PI / 2 / TRANSITION_TIME), 2);
            // Switch back to main backbuffer
            Main.device.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            var bounds = Main.INSTANCE.Window.ClientBounds;
            switch (loadingDirection)
            {
                // Bottom going to top
                case 0:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, -offset+160, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, bounds.Height - offset + 160, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 1:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(offset, 0 + 160, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(offset - bounds.Width, 0 + 160, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                // Top going to bottom
                case 2:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, offset + 160, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, offset - bounds.Height + 160, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 3:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(-offset, 0 + 160, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(bounds.Width - offset, 0 + 160, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
            }

            Main.uiManager.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void SwitchRoom(int direction, Room room)
        {
            loadingDirection = direction;
            loadingRoom = room;
            // Force add player into room. Delete player object if it already had one
            loadingRoom.gameObjects.RemoveAll(item => item is Player);
            foreach(Player player in players){
                loadingRoom.gameObjects.Add(player);
            }
        }

        //loads sound into sfx
        private void LoadSoundEffect(String filename)
        {
            if (filename.IndexOf(".") > 0)
            {
                String name = filename.Substring(0, filename.IndexOf("."));
                string path = "Content/Sound/" + filename;
                if (!File.Exists(path)) throw new FileNotFoundException($"Could not find sound at {path}");
                sfx.Add(name, SoundEffect.FromStream(new FileStream(path, FileMode.Open)));
            }
            else
            {
                Console.WriteLine("Error: invalid name formatting for sound file");
            }
        }

        //changes background music
        public void ChangeMusic(String filename)
        {
            if (filename.IndexOf(".") > 0)
            {
                String name = filename.Substring(0, filename.IndexOf("."));
                string path = "Content/Sound/" + filename;
                if (!File.Exists(path)) throw new FileNotFoundException($"Could not find sound at {path}");
                if(bgm != null){
                    bgm.Pause();
                }
                bgm = SoundEffect.FromStream(new FileStream(path, FileMode.Open)).CreateInstance();
                bgm.IsLooped = true;
                bgm.Play();
            }
            else
            {
                Console.WriteLine("Error: invalid name formatting for sound file");
            }
        }

        public void Mute()
        {
            SoundEffect.MasterVolume = 0.0f;
            muteBit = 1;
        }

        public void Unmute()
        {
            SoundEffect.MasterVolume = 1.0f;
            muteBit = 0;
        }
    }
}
