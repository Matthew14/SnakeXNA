using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        public static Texture2D cellSprite;

        public bool paused;
        public static bool gameover;
        public Grid grid; //main game grid

        //Windows forms components
        public IntPtr drawSurface;
        public System.Windows.Forms.ToolStripLabel scoreLabel;

        public Game1(IntPtr drawSurface, System.Windows.Forms.ToolStripLabel scoreLabel)
        {
            this.drawSurface = drawSurface;
            this.scoreLabel = scoreLabel;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
                        
            //windows forms setup
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

        protected override void Initialize()
        {
            Window.Title = "Snake";
            this.IsMouseVisible = true;
            lastState = Keyboard.GetState();
            content = new ContentManager(Services);
            Content.RootDirectory = "Content";
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundEffect = Content.Load<SoundEffect>("munch");
            cellSprite = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            cellSprite.SetData(new[] { Color.White });
            NewGame();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void NewGame()
        {
            //Make a new grid to fill the screen
            int x = (int)graphics.PreferredBackBufferWidth / Cell.length;
            int y = (int)graphics.PreferredBackBufferHeight / Cell.length;
            grid = new Grid(x, y);
            gameover = false;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            
            if (!paused)
            {
                if (state.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up))
                    grid.snake.Direction = Snake.dir.UP;
                else if (state.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down))
                    grid.snake.Direction = Snake.dir.DOWN;
                else if (state.IsKeyDown(Keys.Right) && lastState.IsKeyUp(Keys.Right))
                    grid.snake.Direction = Snake.dir.RIGHT;
                else if (state.IsKeyDown(Keys.Left) && lastState.IsKeyUp(Keys.Left))
                    grid.snake.Direction = Snake.dir.LEFT;

                grid.Update(soundEffect);
                base.Update(gameTime);
            }
            lastState = state;

            if (!gameover)
                scoreLabel.Text = "Score: " + grid.snake.Eaten.ToString();
            else
            {
                scoreLabel.Text = "GAME OVER! " + "Score: " + grid.snake.Eaten.ToString();
            }
            //Update speed changes as the snake's does (food eaten)
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / grid.snake.Speed);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
