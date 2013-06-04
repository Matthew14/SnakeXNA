using System;

namespace Snake
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Game1 game;
        static void Main(string[] args)
        {
            Form1 form = new Form1();
            form.Show();
            game = new Game1(form.getDrawSurface(), form.getScoreLabel());
            game.Run();
            form.Close();

            game.Exit();
        }
    }
#endif
}

