using _1151.Cross.Util.Outputs;

namespace _1151.Cross.DepedencyInjection.Helpers
{
    public static class OutputsPrinter
    {
        private static readonly IOutput[] _outputs = CrossDI.GetValues<IOutput>()?.ToArray() ?? Array.Empty<IOutput>();

        public static void Print(string text)
        {
            foreach(var output in _outputs)
            {
                output.Print(text);
            }
        }

        public static void Print(string[] texts)
        {
            foreach (var text in texts)
            {
                foreach (var output in _outputs)
                {
                    output.Print(text);
                }
            }
        }
    }
}
