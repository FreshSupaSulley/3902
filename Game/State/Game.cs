﻿using Microsoft.Xna.Framework.Graphics;
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
        private GraphicsDevice device;
        private RenderTarget2D target, loadingTarget;

        // Visible to inheritors
        public Player player;
        public Room room;
        public KeyboardController keyboard;
        public MouseController mouse;

        // Pertains to loading rooms
        private readonly int TRANSITION_TIME = 60;
        private bool renderCaptured;
        private int loadingTime, loadingDirection;
        private Room loadingRoom;

        public static Game instance;

        public static Stopwatch sw;

        //sound
        //sound effects to be played once (i.e. without looping)
        public static Dictionary<String, SoundEffect> sfx = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, SoundEffect> sfxStore = new Dictionary<string, SoundEffect>();
        //currently playing music
        public static SoundEffectInstance bgm;
        public static SoundEffectInstance bgmStore;

        public int muteBit = 0;
        public int muteRequest = 0;

        public Game(GraphicsDevice device)
        {
            this.device = device;
            // Size of Zelda map
            target = new RenderTarget2D(device, (12 + 4) * 16, (7 + 4) * 16);
            loadingTarget = new RenderTarget2D(device, target.Width, target.Height);
            Tile.LoadTextures();
            Door.LoadTextures();
            // Load start room. This also defines the player
            room = Room.LoadRoom("start");
            this.player = (Player)room.gameObjects.Find(entity => entity is Player);
            // Pow
            TempBuffer.pow = Main.Load("pow.png");
            loadSoundEffect("ding.wav");
            loadSoundEffect("punch.wav");
            changeMusic("Song_1.wav");
            sw = new Stopwatch();
            sw.Start();
        }

        public void Update(GameTime gameTime)
        {
            // If we want to switch to pause
            if (Main.INSTANCE.keyboard.IsKeyPressed(Keys.Escape))
            {
                Main.SwitchGameState(new Pause(this));
                return;
            }

            // Tick room if not mid-transition
            if (loadingRoom is null)
            {
                room.Update(this);
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

            if(muteRequest == 1){
                if(muteBit == 0){
                    this.mute();
                }else{
                    this.unmute();
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
                device.SetRenderTarget(target);
                spriteBatch.Begin();
                room.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (renderCaptured is false)
            {
                // Put player in correct location
                switch (loadingDirection)
                {
                    // Top
                    case 0:
                        player.ActiveAnimation = Player.DOWN;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 32 - player.collisionBox.Y);
                        break;
                    // Bottom
                    case 2:
                        player.ActiveAnimation = Player.UP;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 144 - player.collisionBox.Y - player.collisionBox.Height);
                        break;
                    // Left
                    case 3:
                        player.ActiveAnimation = Player.RIGHT;
                        player.Position = new(32 - player.collisionBox.X, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;
                    // Right
                    case 1:
                        player.ActiveAnimation = Player.LEFT;
                        player.Position = new(224 - player.collisionBox.X - player.collisionBox.Width, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;
                }
                renderCaptured = true;
                device.SetRenderTarget(loadingTarget);
                spriteBatch.Begin();
                loadingRoom.Draw(spriteBatch);
                spriteBatch.End();
            }

            // Number of pixels
            double sin = Math.Pow(Math.Sin(loadingTime * Math.PI / 2 / TRANSITION_TIME), 2);
            // Switch back to main backbuffer
            device.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            var bounds = Main.INSTANCE.Window.ClientBounds;
            switch (loadingDirection)
            {
                // Bottom going to top
                case 0:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, -offset, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, bounds.Height - offset, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 1:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(offset, 0, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(offset - bounds.Width, 0, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                // Top going to bottom
                case 2:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, offset, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, offset - bounds.Height, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 3:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(-offset, 0, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(bounds.Width - offset, 0, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
            }

            spriteBatch.End();
        }

        public void SwitchRoom(int direction, Room room)
        {
            loadingDirection = direction;
            loadingRoom = room;
            // Force add player into room. Delete player object if it already had one
            loadingRoom.gameObjects.RemoveAll(item => item is Player);
            loadingRoom.gameObjects.Add(player);
        }

        //loads sound into sfx
        public static void loadSoundEffect(String filename)
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
        public static void changeMusic(String filename)
        {
            if (filename.IndexOf(".") > 0)
            {
                String name = filename.Substring(0, filename.IndexOf("."));
                string path = "Content/Sound/" + filename;
                if (!File.Exists(path)) throw new FileNotFoundException($"Could not find sound at {path}");
                bgm = SoundEffect.FromStream(new FileStream(path, FileMode.Open)).CreateInstance();
                bgm.IsLooped = true;
                bgm.Play();
            }
            else
            {
                Console.WriteLine("Error: invalid name formatting for sound file");
            }
        }

        public void mute(){
            SoundEffect.MasterVolume = 0.0f;
            muteBit = 1;
        }

        public void unmute(){
            SoundEffect.MasterVolume = 1.0f;
            muteBit = 0;
        }

        public static void reset()
        {
            Game.instance.SwitchRoom(1, Room.LoadRoom("start"));
        }
    }
}
