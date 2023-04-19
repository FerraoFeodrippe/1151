namespace _1151.Cross.Util.Outputs
{
    public class ConsoleOutput : IOutput
    {
        public void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}
