﻿namespace _1151.Cross.Outputs
{
    public class ConsoleOutput : IOutput
    {
        public void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}
