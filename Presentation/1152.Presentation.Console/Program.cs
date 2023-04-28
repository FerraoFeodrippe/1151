// See https://aka.ms/new-console-template for more information
using _1151.Core.Application;
using _1151.Cross.DepedencyInjection;
using _1152.Presentation.Console.Constants;

CrossDI.Initialize();

using (var app = new App())
{
    Console.WriteLine(AppConstants.Intro1151);
    Console.WriteLine($"To stop application type at start: {AppConstants.StopCmd}.");

    string actualModule = args.Length > 0 ? args[0] : string.Empty;
    int cmdCheckController = args.Length > 0 ? 1 : 0;
    app.Run(args);

    while (true)
    {
        args = $"{actualModule} {Console.ReadLine()}".Trim().Split(" ").Select(e => e.Trim())
            .Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();

        if (args.Length > 0)
        {
            if (AppConstants.StopCmd.Equals(args[cmdCheckController], StringComparison.InvariantCultureIgnoreCase))
            {
                break;
            }

            if (AppConstants.ResetCmd.Equals(args[cmdCheckController], StringComparison.InvariantCultureIgnoreCase))
            {
                actualModule = string.Empty;
                args = Array.Empty<string>();
                Console.WriteLine("Inputs reseted, please put module at start again.");
                cmdCheckController = 0;
                continue;
            }
            

            if (string.IsNullOrWhiteSpace(actualModule) && app.IsValidModule(args))
            {
                actualModule = args[0];
                Console.WriteLine($"Context Module {actualModule}");
                Console.WriteLine($"To reset module input type at start: {AppConstants.ResetCmd}.");
                cmdCheckController = 1;
            }
            
        }

        app.Run(args);
    }
}
