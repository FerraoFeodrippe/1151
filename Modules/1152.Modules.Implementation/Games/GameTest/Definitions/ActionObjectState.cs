
namespace _1152.Modules.Application.Games.GameTest.Definitions
{
    public class ActionObjectState: IObjectState
    {
        private readonly Action _action;
        private readonly object _locker = new ();

        public ActionObjectState(Action action)
        {
            _action = action;
        }
        public void Update()
        {
            lock(_locker)
            {
                _action.Invoke();
            }
        }
    }
}
