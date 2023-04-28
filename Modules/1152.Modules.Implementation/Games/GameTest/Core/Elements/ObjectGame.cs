using _1152.Modules.Application.Games.GameTest.Definitions;

namespace _1152.Modules.Application.Games.GameTest.Core.Elements
{
    public class ObjectGame: IObjectState
    {
        
        private readonly IObjectState _actionObjectState;

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsVisible { get; set; }

        public char[][] Representation =
{
             "  0  ".ToCharArray(),
             "<(,)>".ToCharArray(),
             "  W  ".ToCharArray()
        };

        public readonly int W;
        public readonly int H;

        public ObjectGame(IObjectState actionObjectState, int positionX, int positionY, bool isVisible)
        {
            W = Representation[0].Length;
            H = Representation.Count();

            _actionObjectState = actionObjectState;
            PositionX = positionX;
            PositionY = positionY;
            IsVisible = false;
        }

        public ObjectGame(IObjectState actionObjectState) : this(actionObjectState, 0, 0, false)
        {
        }


        public ObjectGame(IObjectState actionObjectState, int positionX, int positionY): this(actionObjectState, positionX, positionY, true)
        {
        }

        public void MoveUp(int quantity = 1)
        {
            PositionY -= quantity;
            Update();
        }

        public void MoveDown(int quantity = 1)
        {
            PositionY += quantity;
            Update();
        }

        public void MoveRight(int quantity = 1)
        {
            PositionX += quantity;
            Update();
        }

        public void MoveLeft(int quantity = 1)
        {
            PositionX -= quantity;
            Update();
        }

        public void Update()
        {
            _actionObjectState.Update();
        }
    }
}
