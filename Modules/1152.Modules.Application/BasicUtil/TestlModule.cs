using _1152.Modules.Contracts;

namespace _1152.Modules.Implementation.BasicUtil
{
    public class TestlModule : BaseModule
    {
        public TestlModule(string[] args) : base(args)
        {
        }

        public DateTime GetActualTime()
        {
            return DateTime.Now;
        }
 
    }
}
