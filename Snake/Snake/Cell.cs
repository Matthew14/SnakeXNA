using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Snake
{
    public class Cell
    {
        public static int length = 8; //size of cell in pixels
        private int PosX, PosY; // Cell position

        private Rectangle cellRect;

        //This is to show if the cell is dead or alive:
        public bool SnakeOnMe { get; set; }
        public bool FoodOnMe { get; set; }

        public Cell(int PosX, int PosY)
        {
            this.PosX = PosX;
            this.PosY = PosY;

            cellRect = new Rectangle(PosX, PosY, length, length);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (SnakeOnMe)
                spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Black);
            else
                spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Gray);
            
            if (FoodOnMe)
                spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Blue);
        }

    }
}