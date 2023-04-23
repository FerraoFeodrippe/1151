using _1151.Cross.DepedencyInjection.Helpers;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Contracts;
using System.Reflection;
using System.Threading.Tasks;

namespace _1152.Modules.Implementation
{

    /// <summary>
    /// Base Module to be used as base implementation
    /// </summary>
    public abstract class BaseModule : IModule
    {
        public readonly static string ModuleSufix = "Module";
        protected readonly string[] Args;

        public BaseModule(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Module should have at last 2 values of args.");
            }

            bool isDefinedModule =
                ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule))
                .FirstOrDefault(t => $"{args[0]}{ModuleSufix}".Equals(t.Name, StringComparison.InvariantCultureIgnoreCase)) != null;

            if (!isDefinedModule)
            {
                throw new ArgumentException("Module not defined.");
            }

            Args = args;
        }
        public static string[] GetModulesName()
        {
            return ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule)).Select(t => t.Name.Remove(t.Name.LastIndexOf(ModuleSufix))).ToArray();
        }

        protected MethodInfo[] Methods => ReflectionHelper.GetCustomImplementMethods(GetType());
        protected MethodInfo? Method => ReflectionHelper.GetCustomImplementMethod(GetMethodName(), GetType());

        public string GetModuleName() => Args[0];
        public string GetMethodName() => Args[1];
        public string[] GetModuleMethodsName() => Methods.Select(m => m.Name).ToArray();
        public string[] GetParametersName() => GetParameters().Select(m => m.Name ?? string.Empty).ToArray();
        public string[] GetParametersValues() => Args.Skip(2).ToArray();
        public ParameterInfo[] GetParameters()
        {
            var method = Methods.FirstOrDefault(m => GetMethodName().Equals(m.Name, StringComparison.InvariantCultureIgnoreCase));

            if (method is not null)
            {
                return method.GetParameters();
            }

            return Array.Empty<ParameterInfo>();
        }

        public async void Run()
        {
            if (Method != null)
            {
                OutputsPrinter.Print($"{Method.Name} {string.Join(" ",GetParametersValues())} ");

                var objParams = ReflectionHelper.GetObjectParameters(Method, GetParametersValues());

                object? methodResult = null;

                if (ReflectionHelper.IsMethodAsync(Method))
                {
                    var methodResultAsync = (Task?) Method.Invoke(this, objParams.ToArray());

                    if (methodResultAsync != null)
                    {
                        await methodResultAsync.ConfigureAwait(false);

                        var resultProperty = methodResultAsync.GetType().GetProperty("Result");

                        if  (resultProperty != null)
                        {
                            methodResult = resultProperty.GetValue(methodResultAsync);

                        }
                    }
                }
                else
                {
                    methodResult = Method.Invoke(this, objParams.ToArray());
                }

                if (methodResult != null)
                {
                    OutputsPrinter.Print($"Result:");

                    if (methodResult.GetType().IsArray)
                    {    
                        foreach(var r in (Array)methodResult)
                        {
                            OutputsPrinter.Print(r.ToString() ?? string.Empty);
                        }

                        if (((Array)methodResult).Length == 0)
                        {
                            OutputsPrinter.Print("No result.");
                        }
                    }
                    else 
                    {
                        OutputsPrinter.Print(methodResult.ToString() ?? "No result.");
                    }
                }
                else
                {
                    OutputsPrinter.Print("Executed.");
                }

            }
        }
    }
}
