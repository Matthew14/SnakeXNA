using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Snake
{
    public class Snake
    {
        private int speed = 10; //used in Game1.Update
        public int Speed { get { return speed; } }

        private int eaten = 0;
        public int Eaten { get { return eaten; } }

        private bool moving, growing;
        private int length = 5; //length of snake in rects
        private int cellSize;
        private LinkedList<Rectangle> body; // the squares making up the body first = head etc.

        public enum dir {UP, DOWN, RIGHT, LEFT};
        
        private dir direction;
        public dir Direction
        {
            get { return direction; }
            set 
            {
                //this prevents direction being set to left if already going 
                //right and so on
                switch (value)
                {
                    case dir.UP:
                        if (direction != dir.DOWN)
                            direction = value;
                        break;

                    case dir.DOWN:
                        if (direction != dir.UP)
                            direction = value;
                        break;

                    case dir.LEFT:
                        if (direction != dir.RIGHT)
                            direction = value;
                        break;

                    case dir.RIGHT:
                        if (direction != dir.LEFT)
                            direction = value;
                        break;

                    default:
                        throw new NotSupportedException("Direction must be left, right, up or down");
                }
            }
        }
        
        public Snake(Cell[,] cellArray, int cellSize)
        {
            this.cellSize = cellSize;
            moving = true;

            Random r = new Random(DateTime.Now.Millisecond);
            int startPos = r.Next(0, cellArray.GetLength(1) - this.length);
            direction = dir.UP;

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
                    case dir.UP: 
                        if (body.First.Value.Y - 1 >= 0)
                            toAdd = new Rectangle(body.First.Value.X,
                                body.First.Value.Y - 1, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(body.First.Value.X,
                                cellArray.GetLength(1) - 1, cellSize, cellSize);
                        break;
                    case dir.RIGHT:
                        if (body.First.Value.X + 1 < cellArray.GetLength(0))
                            toAdd = new Rectangle(body.First.Value.X + 1,
                                body.First.Value.Y, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(0,
                                body.First.Value.Y, cellSize, cellSize);
                        break;
                    case dir.DOWN: 
                        if (body.First.Value.Y + 1 < cellArray.GetLength(1))
                            toAdd = new Rectangle(body.First.Value.X,
                                body.First.Value.Y + 1, cellSize, cellSize);
                        else
                            toAdd = new Rectangle(body.First.Value.X,
                                0, cellSize, cellSize);
                        break;
                    case dir.LEFT:
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
                {
                    body.AddFirst(toAdd);
                    cellArray[toAdd.X, toAdd.Y].SnakeOnMe = true;
                }

                if (!growing)
                {
                    cellArray[body.Last.Value.X, body.Last.Value.Y].SnakeOnMe = false;
                    body.RemoveLast();
                }
                growing = false;

            }
        }
        
    }
}
