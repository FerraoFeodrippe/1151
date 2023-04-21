// See https://aka.ms/new-console-template for more information
using _1151.Core.Application;
using _1151.Cross.DepedencyInjection;

CrossDI.Initialize();

using (var app = new App())
{
    app.Run(args);

    string[] newArgs;
    while (true)
    {
        newArgs = Console.ReadLine()?.Trim().Split(" ").Select(e => e.Trim())
            .Where(e => !string.IsNullOrWhiteSpace(e)).ToArray() ?? Array.Empty<string>();

        if (newArgs.Length == 0)
        {
            break;
        }

        if ("-stop".Equals(newArgs[0], StringComparison.InvariantCultureIgnoreCase))
        {
            break;
        }

        app.Run(newArgs);
    }
}
