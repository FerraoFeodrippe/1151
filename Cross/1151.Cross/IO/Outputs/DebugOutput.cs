using System.Diagnostics;

namespace _1151.Cross.Outputs
{
    public class DebugOutput : IOutput
    {
        public void Print(string text)
        {
            Debug.WriteLine(text);
        }
    }
}
