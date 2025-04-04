﻿using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Game.Controllers;
using Microsoft.Xna.Framework;
using Game.State;
using Game.Util;

new Main().Run();

public class Main : Microsoft.Xna.Framework.Game
{
    // Current game state (game, menu, pause, etc.)
    private IGameState state;

    // Ratio
    public static Vector2 BASE_TO_WINDOW;

    // Used to load resources statically
    public static GraphicsDevice device;
    private GraphicsDeviceManager graphics;
    private RenderTarget2D target, loadingTarget;

    // Used for rendering everything
    private SpriteBatch spriteBatch;
    public SpriteFont font;

    public KeyboardController keyboard;
    public MouseController mouse;

    public static Main INSTANCE;

    public Main()
    {
        INSTANCE = this;
        graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1600 / 2,
            PreferredBackBufferHeight = 1100 / 2
        };
        // Window.AllowUserResizing = true;
        Window.Title = "Bombardier Beetles - Sprint 4";
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Size of Zelda map
        target = new RenderTarget2D(graphics.GraphicsDevice, (12 + 4) * 16, (7 + 4) * 16);
        loadingTarget = new RenderTarget2D(graphics.GraphicsDevice, target.Width, target.Height);

        BASE_TO_WINDOW = new Vector2(graphics.PreferredBackBufferWidth / target.Bounds.Width, graphics.PreferredBackBufferHeight / target.Bounds.Height);
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Controllers
        keyboard = new KeyboardController();
        mouse = new MouseController();

        // This calls load content
        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Used to load resources statically
        device = graphics.GraphicsDevice;
        font = Content.Load<SpriteFont>("Font");
        // state = new World(device);
        state = new Menu(device);
    }

    // Tick
    protected override void Update(GameTime gameTime)
    {
        // Probably unnecessary?
        base.Update(gameTime);

        // Always update inputs first
        keyboard.Update(gameTime);
        mouse.Update(gameTime);

        // Update current state
        state.Update(gameTime);

        // Document how much time has passed (probably not how I'm supposed to do it but I'm grasping at straws here :/ - Ty) 
        TempBuffer.elapsed = gameTime.ElapsedGameTime.Milliseconds;
        // Remove any expired temporary entities
        TempBuffer.depreciate(this);

        // Always end with post ticks
        keyboard.PostUpdate();
        mouse.PostUpdate();
    }

    // Render
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Draw game state
        state.Draw(spriteBatch);

        base.Draw(gameTime);
    }

    public static void SwitchGameState(IGameState state)
    {
        // Invoke enter / exit
        INSTANCE.state.OnExit();
        state.OnEnter();
        // Do the switching
        INSTANCE.state = state;
    }

    // Loading textures statically
    public static Texture2D Load(string path)
    {
        // Check if the file exists
        string fullPath = "Content/Sprites/" + path;
        if (!File.Exists(fullPath)) throw new FileNotFoundException($"Could not find texture at {fullPath}");
        // Try with resources
        using var fileStream = new FileStream(fullPath, FileMode.Open);
        return Texture2D.FromStream(device, fileStream);
    }

    // Loading textures with subimage
    public static Texture2D Load(string path, Rectangle subimage) => Subimage(Load(path), subimage);

    // Grabs a subimage from a texture
    public static Texture2D Subimage(Texture2D texture, Rectangle subimage)
    {
        Texture2D croppedTexture = new(device, subimage.Width, subimage.Height);
        Color[] data = new Color[subimage.Width * subimage.Height];
        texture.GetData(0, subimage, data, 0, data.Length);
        croppedTexture.SetData(data);
        return croppedTexture;
    }
}