using _1152.Cross.Util.Helpers;
using _1152.Modules.Implementation;

namespace _1152.Modules.Application._1151Helper
{
    /// <summary>
    /// Helper Moduler to help with app
    /// </summary>
    public class HModule : BaseModule
    {
        public HModule(string[] args) : base(args)
        {
        }

        public string[] Modules()
        {
            return GetModulesName();
        }

        public new string[] Methods(string moduleName)
        {
           var moduleType= ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule))
               .FirstOrDefault(t => $"{moduleName}{ModuleSufix}".Equals(t.Name, StringComparison.InvariantCultureIgnoreCase));

            if (moduleType != null)
            {
                return ReflectionHelper.GetCustomImplementMethods(moduleType).Select(e=> e.Name).ToArray();
            }

            return Array.Empty<string>();
        }

        public string[] Parameters(string moduleName, string methodName)
        {

            var moduleType = ReflectionHelper.GetTypesIsSubclassOf(typeof(BaseModule))
                .FirstOrDefault(t => $"{moduleName}{ModuleSufix}".Equals(t.Name, StringComparison.InvariantCultureIgnoreCase));

            if (moduleType != null)
            {
                var method = ReflectionHelper.GetCustomImplementMethod(methodName, moduleType);

                if (method != null)
                {
                    return method.GetParameters()?.Select(e=> $"{e.Name}: {e.ParameterType.Name}").ToArray() ?? Array.Empty<string>();
                }
            }

            return Array.Empty<string>();
        }
    }
}
