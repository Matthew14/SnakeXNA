using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Snake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;
        KeyboardState lastState;

        bool paused;
        
        public static Texture2D cellSprite;

        
        public Grid grid;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.AllowUserResizing = true;
            content = new ContentManager(Services);
            Content.RootDirectory = "Content";
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            paused = true;
            grid = new Grid(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "My Snake";
            this.IsMouseVisible = true;

            lastState = Keyboard.GetState();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cellSprite = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            cellSprite.SetData(new[] { Color.White });
            int x = (int)graphics.PreferredBackBufferWidth / Cell.length;
            int y = (int)graphics.PreferredBackBufferHeight / Cell.length;
            grid = new Grid(x, y);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();
            if (state.IsKeyDown(Keys.P) && lastState.IsKeyUp(Keys.P))
                paused = !paused;

            if (!paused)
            {
                if (state.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up))
                    grid.snake.Direction = 1;
                if (state.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down))
                    grid.snake.Direction = 3;
                if (state.IsKeyDown(Keys.Right) && lastState.IsKeyUp(Keys.Right))
                    grid.snake.Direction = 2;
                if (state.IsKeyDown(Keys.Left) && lastState.IsKeyUp(Keys.Left))
                    grid.snake.Direction = 4;
            }
            lastState = state;

            // TODO: Add your update logic here
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / grid.snake.speed);
            if (!paused)
            {
                grid.Update();

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
            
        }
    }
}
