using _1151.Cross.DepedencyInjection;
using _1151.Cross.Util.Helpers;
using _1151.Cross.Util.Outputs;
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
        private readonly IOutput[] _outputs;

        protected readonly string[] Args;

        public BaseModule(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Module should have at last 2 values of args.");
            }

            bool isDefinedModule =
                AssemblyHelper.GetTypesIsSubclassOf(typeof(BaseModule)).FirstOrDefault(t => $"{args[0]}{ModuleSufix}".Equals(t.Name)) != null;

            if (!isDefinedModule)
            {
                throw new ArgumentException("Module not defined.");
            }

            _outputs = CrossDI.GetValues<IOutput>()?.ToArray() ?? Array.Empty<IOutput>();

            Args = args;
        }
        public static string[] GetModulesName()
        {
            return AssemblyHelper.GetTypesIsSubclassOf(typeof(BaseModule)).Select(t => t.Name).ToArray();
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

        protected MethodInfo[] Methods => AssemblyHelper.GetCustomImplementMethods(GetType());
        protected MethodInfo? Method => AssemblyHelper.GetCustomImplementMethod(MethodName, GetType());

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

        private void Print(string text)
        {
            OutputsPrinter.Print(text, _outputs);
        }

        public void Run()
        {
            Print(Name);
            Print("________________");
            Print(MethodName);
            Print("________________");

            foreach (var method in MethodsName)
            {
                Print(method);
            }

            Print("________________");

            foreach (var parameter in GetParameters())
            {
                Print($"{parameter.Name}:{parameter.ParameterType.Name}");
            }

            Print("________________");

            if (Method != null)
            {
                List<object?> objParams = new(Method.GetParameters().Length);
                var paramValues = GetParametersValues();
                int i = 0;
                foreach (var param in Method.GetParameters())
                {
                    objParams.Add(Convert.ChangeType(paramValues[i], param.ParameterType));
                    i++;
                }

                var result = Method.Invoke(this, objParams.ToArray());

                Print("Result: ");
                Print(result?.ToString() ?? string.Empty);
            }
        }
    }
}
