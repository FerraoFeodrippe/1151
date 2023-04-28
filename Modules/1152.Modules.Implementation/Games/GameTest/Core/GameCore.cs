using _1152.Modules.Application.Games.GameTest.Core.Draw;
using _1152.Modules.Application.Games.GameTest.Core.Elements;
using _1152.Modules.Application.Games.GameTest.Core.Stages;
using _1152.Modules.Application.Games.GameTest.Definitions;
using System.Threading;

namespace _1152.Modules.Application.Games.GameTest.Core
{
    public class GameCore: IDisposable
    {
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("GameTest Finished.");
        }

        private readonly char [][] _field;
        private readonly int _maxH;
        private readonly int _maxW;

        private CancellationTokenSource _cancellationTokenSource;
        private volatile int _cmd;
        private int _frequency;
        private readonly object _locker = new object();

        private ObjectGame _player;
        private ObjectGame _playerLastPosition;

        protected IObjectState DrawUpdate;

        public GameCore()
        {
            Console.Clear();
            _cancellationTokenSource = new CancellationTokenSource();
            _frequency = 5;
            _cmd = 0;
            Console.CursorVisible = false;

            _maxH = Console.BufferHeight;
            _maxW = Console.BufferWidth;
            _field = new char[_maxH][];

            DrawUpdate = new ActionObjectState(() =>
            {
                lock (_locker)
                {
                    Stage1.Load(_field, _maxW, _maxH);
                    UpdatePlayerPosition();
                    DrawFields.Draw(_field, _maxW, _maxH);
                }
            });

            _player = new ObjectGame(DrawUpdate, _maxW / 2, _maxH/2);
            _playerLastPosition = new ObjectGame(DrawUpdate, _player.PositionX, _player.PositionY);

            for (int i = 0; i < _maxH; i++)
            {
                _field[i] = Enumerable.Repeat(char.MinValue, _maxW).ToArray();
            }

            FillFieldObject(_player);

            DrawUpdate.Update();
        }

        private void UpdatePlayerPosition()
        {
            lock (_locker)
            {
                if ((_player.PositionX >= _maxW - _player.W  || _player.PositionX <= 0) ||
                (_player.PositionY >= _maxH - _player.H || _player.PositionY <= 0))
                {
                    _player.PositionX = _playerLastPosition.PositionX;
                    _player.PositionY = _playerLastPosition.PositionY;
                }
                else
                {
                    FillFieldObject(_player, _playerLastPosition);
                    _playerLastPosition.PositionX = _player.PositionX;
                    _playerLastPosition.PositionY = _player.PositionY;
                }
            }
        }


        public void FillFieldObject(ObjectGame objectGame)
        {
            for (int i = objectGame.PositionY; i < objectGame.H + objectGame.PositionY; i++)
            {
                for (int j = objectGame.PositionX; j < objectGame.W + objectGame.PositionX; j++)
                {
                    char pR = objectGame.Representation[i - objectGame.PositionY][j - objectGame.PositionX];

                    _field[i][j] = pR != char.MinValue && pR != ' ' ?
                        pR : _field[i][j];
                }
            }
        }

        public void FillFieldObject(ObjectGame objectGame, ObjectGame objectGameLastPosition)
        {
            for (int i = objectGameLastPosition.PositionY; i < objectGameLastPosition.H + objectGameLastPosition.PositionY; i++)
            {
                for (int j = objectGameLastPosition.PositionX; j < objectGameLastPosition.W + objectGameLastPosition.PositionX; j++)
                {
                    _field[i][j] = ' ';
                }
            }

            FillFieldObject(objectGame);
        }

        public void ReadKey()
        {
            lock (_locker)
            {
                var keyPressed = Console.ReadKey(true).Key;

                if (keyPressed == ConsoleKey.Escape)
                {
                    _cancellationTokenSource.Cancel();
                    _cmd = -1;
                }

                if (keyPressed == ConsoleKey.W)
                {
                    _player.MoveUp();
                }

                if (keyPressed == ConsoleKey.S)
                {
                    _player.MoveDown();
                }

                if (keyPressed == ConsoleKey.A)
                {
                    _player.MoveLeft();
                }

                if (keyPressed == ConsoleKey.D)
                {
                    _player.MoveRight();
                }
            }
        }

        public void Run()
        {
            var cancelToken = _cancellationTokenSource.Token;

            Task.Run(() => 
            { 
                while (!cancelToken.IsCancellationRequested)
                {
                    ReadKey();
                    Thread.Sleep(_frequency);
                } 
            }, cancelToken);


            while (_cmd > -1)
            {
                Thread.Sleep(500);
            }

            _cancellationTokenSource.Cancel();
            Console.Clear();
        }
    }
}
