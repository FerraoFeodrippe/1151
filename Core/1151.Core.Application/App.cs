using _1151.Core.Application.Validations;
using _1151.Cross.DepedencyInjection.Helpers;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Contracts;
using _1152.Modules.Implementation;

namespace _1151.Core.Application
{
    public static class App
    {
        public static void Run(string[] args)
        {
            try
            {
                var resultValidation = ValidationManager.Start(args);

                if (resultValidation.IsOk)
                {
                    var type = ReflectionHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

                    if (type != null)
                    {
                        IModule? module = (IModule?) Activator.CreateInstance(type, new object?[] { args });
                        module?.Run();
                    }
                }
                else
                {
                    OutputsPrinter.Print("Can not execute, errors: ");
                    foreach(var error in resultValidation.Errors)
                    {
                        OutputsPrinter.Print(error);
                    }
                }
            }
            catch(Exception ex)
            {
                OutputsPrinter.Print("No expected Error!");
                OutputsPrinter.Print("_________________");
                OutputsPrinter.Print(ex.Message);
            }
            
        }
    }
}
