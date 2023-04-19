using _1152.Core.AssemblyInfo.Helpers;
using _1152.Modules.Contracts;
using System.Reflection;

namespace _1152.Modules.Application
{

    /// <summary>
    /// Base Module to be used as base implementation
    /// </summary>
    public abstract class BaseModule : IModule
    {
        private readonly string ModuleSufix = "Module";

        protected readonly string[] Args;

        public static string[] GetModulesName()
        {
            return AssemblyHelper.GetTypesIsSubclassOf(typeof(BaseModule)).Select(t => t.Name).ToArray();
        }

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

            Args = args;
        }

        /// <summary>
        /// Module's name
        /// </summary>
        public string Name => Args[0];

        /// <summary>
        /// Module's name
        /// </summary>
        public string MethodName => Args[1];

        protected MethodInfo[] Methods => AssemblyHelper.CustomImplementMethods(GetType());

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
            //TODO: need implement to run specific method
        }
    }
}
