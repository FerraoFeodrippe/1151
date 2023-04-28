using _1152.Modules.Application.Games.GameTest.Core;
using _1152.Modules.Implementation;

namespace _1152.Modules.Application.Games.GameTest
{
    public class GameTestModule : BaseModule
    {
        public GameTestModule(string[] args) : base(args)
        {
        }

        public void Start()
        {
            using (var gameCore = new GameCore())
            {
                gameCore.Run();
            }
        }
    }
}
