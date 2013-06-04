using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Snake
{
    public class Grid
    {
        private int length = Cell.length;
        private int sizeX, sizeY;
        public Cell[,] gridArray;
        public Snake snake;
        
        public Grid() : this(50, 50){}

        public Grid(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            gridArray = new Cell[this.sizeX, this.sizeY];
            FillGrid();
            snake = new Snake(gridArray, length);

            Food();//first food piece 
        }
        public void Food() 
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int x = r.Next(0, sizeX);
            int y = r.Next(0, sizeY);
            while (gridArray[x, y].SnakeOnMe)//food can't spawn on the snake
            {
                x = r.Next(0, sizeX);
                y = r.Next(0, sizeY);
            }
            gridArray[x, y].FoodOnMe = true;
        }

        public void FillGrid()
        {
            for (int i = 0; i < sizeX; ++i)
            {
                for (int j = 0; j < sizeY; ++j)
                    gridArray[i, j] = new Cell(i * length, j * length);
            }
        }
        
        public void Update(SoundEffect se)
        {
            foreach (Cell c in gridArray)
            {
                if (c.FoodOnMe && c.SnakeOnMe)//snake eats the food
                {
                    se.Play();
                    c.FoodOnMe = false;

                    Food();
                    snake.Grow();
                }
            }
            snake.Update(gridArray);
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Cell c in gridArray)
            {
                c.Draw(spritebatch);
            }
        }
    }
}
