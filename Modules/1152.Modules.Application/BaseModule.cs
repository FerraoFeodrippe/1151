using _1151.Cross.DepedencyInjection.Helpers;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Contracts;
using System.Reflection;

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
                ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule)).FirstOrDefault(t => $"{args[0]}{ModuleSufix}".Equals(t.Name)) != null;

            if (!isDefinedModule)
            {
                throw new ArgumentException("Module not defined.");
            }

            Args = args;
        }
        public static string[] GetModulesName()
        {
            return ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule)).Select(t => t.Name).ToArray();
        }

        /// <summary>
        /// Module's name
        /// </summary>
        public string Name => Args[0];

        /// <summary>
        /// Module's name
        /// </summary>
        public string MethodName => Args[1];

        public string[] GetParametersValues() => Args.Skip(2).ToArray();

        protected MethodInfo[] Methods => ReflectionHelper.GetCustomImplementMethods(GetType());
        protected MethodInfo? Method => ReflectionHelper.GetCustomImplementMethod(MethodName, GetType());

        protected ParameterInfo[] GetParameters()
        {
            var method = Methods.FirstOrDefault(m => MethodName.Equals(m.Name, StringComparison.InvariantCultureIgnoreCase));

            if (method is not null)
            {
                return method.GetParameters();
            }

            return Array.Empty<ParameterInfo>();
        }

        /// <summary>
        /// Module's methods
        /// </summary>
        public string[] MethodsName => Methods.Select(m => m.Name).ToArray();

        /// <summary>
        /// Method's parameters
        /// </summary>
        public string[] ParametersName => GetParameters().Select(m => m.Name ?? string.Empty).ToArray();


        public void Run()
        {
            OutputsPrinter.Print(Name);
            OutputsPrinter.Print("________________");
            OutputsPrinter.Print(MethodName);
            OutputsPrinter.Print("________________");

            foreach (var method in MethodsName)
            {
                OutputsPrinter.Print(method);
            }

            OutputsPrinter.Print("________________");

            foreach (var parameter in GetParameters())
            {
                OutputsPrinter.Print($"{parameter.Name}:{parameter.ParameterType.Name}");
            }

            OutputsPrinter.Print("________________");

            if (Method != null)
            {
                var objParams = ReflectionHelper.GetObjectParameters(Method, GetParametersValues());
                var result = Method.Invoke(this, objParams.ToArray());

                OutputsPrinter.Print("Result: ");
                OutputsPrinter.Print(result?.ToString() ?? string.Empty);
            }
        }
    }
}
