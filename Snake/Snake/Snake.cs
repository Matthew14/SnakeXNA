using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snake
{
    public class Snake
    {
        public int speed = 10; //used in Game1.Update()
        private int length = 5; //length of snake in rects
        public int eaten = 0;
        private int cellSize;
        private LinkedList<Rectangle> body;
        public bool moving, growing;

        private int direction; // 1 - up, 2 - right 3 - down, 4 left

        public int Direction
        {
            get { return direction; }
            set 
            {
                //this prevents direction being set to left if already going 
                //right and so on
                switch (value)
                {
                    case 1:
                        if (direction != 3)
                            direction = value;
                        break;

                    case 2:
                        if (direction != 4)
                            direction = value;
                        break;

                    case 3:
                        if (direction != 1)
                            direction = value;
                        break;

                    case 4:
                        if (direction != 2)
                            direction = value;
                        break;

                    default:
                        throw new NotSupportedException("Direction must be between 1 and 4 (inclusive)");
                }
            }
        }
        
        public Snake(Cell[,] cellArray, int cellSize)
        {
            this.cellSize = cellSize;
            moving = true;

            Random r = new Random(DateTime.Now.Millisecond);
            int startPos = r.Next(0, cellArray.GetLength(1) - this.length);
            direction = 1;

            body = new LinkedList<Rectangle>();
            body.AddFirst(new Rectangle(startPos, startPos, cellSize, cellSize));
            for (int i = 0; i < length; ++i)
                body.AddLast(new Rectangle(startPos + i, startPos, cellSize, cellSize));
        }
        public Snake(Cell[,] cellArray): this(cellArray, 5) { }

        public void Grow()
        {

            speed++;
            eaten++;
            growing = true;
        }
                
        public void Update(Cell[,] cellArray)
        {
            if (moving)
            {
                Rectangle toAdd = new Rectangle();
                switch (direction)
                {
                    case 1: // up
                        if (body.First.Value.Y - 1 >= 0)
                            toAdd = new Rectangle(body.First.Value.X,
                                body.First.Value.Y - 1, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(body.First.Value.X,
                                cellArray.GetLength(1) - 1, cellSize, cellSize);
                        break;
                    case 2: // right
                        if (body.First.Value.X + 1 < cellArray.GetLength(0))
                            toAdd = new Rectangle(body.First.Value.X + 1,
                                body.First.Value.Y, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(0,
                                body.First.Value.Y, cellSize, cellSize);
                        break;
                    case 3: // down 
                        if (body.First.Value.Y + 1 < cellArray.GetLength(1))
                            toAdd = new Rectangle(body.First.Value.X,
                                body.First.Value.Y + 1, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(body.First.Value.X,
                                0, cellSize, cellSize);
                        break;
                    case 4: // left
                        if (body.First.Value.X - 1 >= 0)
                            toAdd = new Rectangle(body.First.Value.X - 1,
                                body.First.Value.Y, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(cellArray.GetLength(0) - 1,
                                body.First.Value.Y, cellSize, cellSize);
                        break;
                    default:
                        break;
                }
                foreach (Rectangle r in body)
                {
                    if (toAdd == r)
                    {
                        moving = false;
                        Game1.gameover = true;
                    }
                }
                if (moving)
                    body.AddFirst(toAdd);

                if (!growing)
                {
                    cellArray[body.Last.Value.X, body.Last.Value.Y].SnakeOnMe = false;
                    body.RemoveLast();
                }
                growing = false;

                foreach (Rectangle r in body)
                    cellArray[r.X, r.Y].SnakeOnMe = true;
            }
        }
        
    }
}
