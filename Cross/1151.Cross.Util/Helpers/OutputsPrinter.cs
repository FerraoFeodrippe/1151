using _1151.Cross.Util.Outputs;

namespace _1151.Cross.Util.Helpers
{
    public static class OutputsPrinter
    {
        public static void Print(string text, IOutput[] outputs)
        {
            foreach(var output in outputs)
            {
                output.Print(text);
            }
        }
    }
}
