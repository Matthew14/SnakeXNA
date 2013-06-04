using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snake
{
    public class Cell
    {
        public static int length = 10; //size of cell in pixels
        private int PosX, PosY; // Cell position

        private Rectangle cellRect;

        public bool SnakeOnMe { get; set; }
        public bool FoodOnMe { get; set; }

        public Cell(int PosX, int PosY)
        {
            this.PosX = PosX;
            this.PosY = PosY;

            cellRect = new Rectangle(PosX, PosY, length, length);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                if (SnakeOnMe)
                    spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Black);
                else if (FoodOnMe)
                    spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Blue);
                else
                {
                    if (!Game1.gameover)
                        spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Gray);
                    else
                        spriteBatch.Draw(Game1.cellSprite, cellRect, Color.DarkRed);
                }
        }
    }
}