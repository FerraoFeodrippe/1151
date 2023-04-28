using _1152.Modules.Application.Games.GameTest.Core.Draw;
using _1152.Modules.Application.Games.GameTest.Core.Elements;
using _1152.Modules.Application.Games.GameTest.Core.Stages;
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

        private ObjectGame _player;
        private ObjectGame _playerLastPosition;

        private readonly object _locker = new object();

        public GameCore()
        {
            Console.Clear();
            _cancellationTokenSource = new CancellationTokenSource();
            _frequency = 20;
            _cmd = 0;
            Console.CursorVisible = false;

            _maxH = Console.BufferHeight;
            _maxW = Console.BufferWidth;
            _player = new ObjectGame(_maxW/2, _maxH/2);
            _playerLastPosition = new ObjectGame(_player.PositionX, _player.PositionY);

            _field = new char[_maxW][];
            for (int i = 0; i < _maxW; i++)
            {
                _field[i] = Enumerable.Repeat(char.MinValue, _maxW).ToArray();
            }

            _field[_player.PositionX][_player.PositionY] = '0';
        }

        private void UpdatePlayerPosition()
        {
            if ((_player.PositionX >= _maxW - 1 || _player.PositionX <= 0) ||
                (_player.PositionY >= _maxH - 1 || _player.PositionY <= 0))
            {
                _player.PositionX = _playerLastPosition.PositionX;
                _player.PositionY = _playerLastPosition.PositionY;
            }
            else
            {
                _field[_playerLastPosition.PositionX][_playerLastPosition.PositionY] = ' ';
                _field[_player.PositionX][_player.PositionY] = '0';
                _playerLastPosition.PositionX = _player.PositionX;
                _playerLastPosition.PositionY = _player.PositionY;
            }
        }

        public void ReadKey()
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
                UpdatePlayerPosition();
            }

            if (keyPressed == ConsoleKey.S)
            {
                _player.MoveDown();
                UpdatePlayerPosition();
            }

            if (keyPressed == ConsoleKey.A)
            {
                _player.MoveLeft();
                UpdatePlayerPosition();
            }

            if (keyPressed == ConsoleKey.D)
            {
                _player.MoveRight();
                UpdatePlayerPosition();
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
                } 
            }, cancelToken);


            Task.Run(() =>
            {
                Stage1.Load(_field, _maxW, _maxH);
                while (!cancelToken.IsCancellationRequested)
                {
                    DrawFields.Draw(_field, _maxW, _maxH);
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
