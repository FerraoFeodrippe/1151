using _1151.Cross.DepedencyInjection;
using _1151.Cross.Util.Helpers;
using _1151.Cross.Util.Outputs;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Contracts;
using _1152.Modules.Implementation;

namespace _1151.Core.Application
{
    public class App
    {
        private readonly IOutput[] _outputs;

        public App()
        {
            _outputs = CrossDI.GetValues<IOutput>()?.ToArray() ?? Array.Empty<IOutput>();
        }

        private void Print(string text)
        {
            OutputsPrinter.Print(text, _outputs);
        }

        public void Run(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Print($"Module should have at last 2 values of args.");
                    return;
                }

                var type = AssemblyHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

                if (type == null)
                {
                    Print($"There is not such module {args[0]}.");
                    return;
                }

                var method = AssemblyHelper.GetCustomImplementMethod(args[1], type);

                if (method == null)
                {
                    Print($"There is not such method {args[1]} for this module {args[0]}.");
                    return;
                }

                if (method.GetParameters().Length > args.Length - 2)
                {
                    Print($"Need at last {method.GetParameters().Length} arguments for method {args[1]}.");
                    return;
                }

                IModule? module = (IModule?)Activator.CreateInstance(type, new object?[] { args });

                module?.Run();
            }
            catch(Exception ex)
            {
                Print(ex.Message);
            }
            
        }
    }
}
