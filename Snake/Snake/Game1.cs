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
        SoundEffect soundEffect;

        public bool paused;
        public static bool gameover;
        
        public static Texture2D cellSprite;

        public IntPtr drawSurface;
        public System.Windows.Forms.ToolStripLabel scoreLabel;
        public Grid grid;

        public Game1(IntPtr drawSurface, System.Windows.Forms.ToolStripLabel scoreLabel)
        {
            this.drawSurface = drawSurface;
            this.scoreLabel = scoreLabel;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            Window.AllowUserResizing = true;
            content = new ContentManager(Services);
            Content.RootDirectory = "Content";
            
            graphics.PreparingDeviceSettings +=
            new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged +=
            new EventHandler(Game1_VisibleChanged);

            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
                e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
                drawSurface;
        }
 
        /// <summary>
        /// Occurs when the original gamewindows' visibility changes and makes sure it stays invisible
        /// </summary>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
                if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                    System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
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
            
            Window.Title = "My Snake";
            this.IsMouseVisible = true;

            lastState = Keyboard.GetState();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundEffect = Content.Load<SoundEffect>("munch");
            cellSprite = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            cellSprite.SetData(new[] { Color.White });
            int x = (int)graphics.PreferredBackBufferWidth / Cell.length;
            int y = (int)graphics.PreferredBackBufferHeight / Cell.length;
            grid = new Grid(x, y);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void NewGame()
        {
            int x = (int)graphics.PreferredBackBufferWidth / Cell.length;
            int y = (int)graphics.PreferredBackBufferHeight / Cell.length;
            grid = new Grid(x, y);
            gameover = false;
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState state = Keyboard.GetState();
            
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
                grid.Update(soundEffect);
                base.Update(gameTime);
            }
            if (!gameover)
                scoreLabel.Text = "Score: " + grid.snake.eaten.ToString();
            else
                scoreLabel.Text = "GAME OVER!";
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
