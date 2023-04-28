using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1152.Modules.Application.Games.GameTest.Core.Elements
{
    public class ObjectGame
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsVisible { get; set; }

        public ObjectGame()
        {
            PositionX = 0;
            PositionY = 0;
            IsVisible = false;
        }

        public ObjectGame(int positionX, int positionY, bool isVisible)
        {
            PositionX = positionX;
            PositionY = positionY;
            IsVisible = false;
        }

        public ObjectGame(int positionX, int positionY): this(positionX, positionY, true)
        {
        }

        public void MoveUp(int quantity = 1)
        {
            PositionY -= quantity;
        }

        public void MoveDown(int quantity = 1)
        {
            PositionY += quantity;
        }

        public void MoveRight(int quantity = 1)
        {
            PositionX += quantity;
        }

        public void MoveLeft(int quantity = 1)
        {
            PositionX -= quantity;
        }

    }
}
