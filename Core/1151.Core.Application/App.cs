using _1151.Core.Application.Constants;
using _1151.Core.Application.Validations;
using _1151.Cross.DepedencyInjection.Helpers;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Contracts;
using _1152.Modules.Implementation;

namespace _1151.Core.Application
{
    public class App: IDisposable
    {
        private static readonly CancellationTokenSource _tokenSource = new();

        public void Run(string[] args)
        {
            try 
            {
                Task.Run(() =>
                {
                    try
                    {
                        var resultValidation = ValidationManager.Start(args);

                        if (resultValidation.IsOk)
                        {
                            var type = ReflectionHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

                            if (type != null)
                            {
                                IModule? module = (IModule?)Activator.CreateInstance(type, new object?[] { args });

                                if (module != null)
                                {

                                    OutputsPrinter.Print(module.GetModuleName());
                                    OutputsPrinter.Print(module.GetMethodName());

                                    foreach (var parameter in module.GetParameters())
                                    {
                                        OutputsPrinter.Print($"{parameter.Name}:{parameter.ParameterType.Name}");
                                    }

                                    OutputsPrinter.Print(AppConstants.Delimiter);

                                    module.Run();
                                }
                            }
                        }
                        else
                        {
                            OutputsPrinter.Print("Can not execute, errors: ");
                            OutputsPrinter.Print(resultValidation.Errors);
                        }
                    }
                    catch (Exception ex)
                    {
                        OutputsPrinter.Print("No expected Error!");
                        OutputsPrinter.Print(AppConstants.Delimiter);
                        OutputsPrinter.Print(ex.Message);
                    }
                }, _tokenSource.Token);

            }
            catch (Exception ex)
            {
                OutputsPrinter.Print("No expected Error!");
                OutputsPrinter.Print(AppConstants.Delimiter);
                OutputsPrinter.Print(ex.Message);
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
