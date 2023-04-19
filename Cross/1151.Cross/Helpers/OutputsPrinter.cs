using _1151.Cross.Outputs;

namespace _1151.Cross.Helpers
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
