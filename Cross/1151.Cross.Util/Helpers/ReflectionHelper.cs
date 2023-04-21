using System.Reflection;

namespace _1152.Cross.Util.Helpers
{
    public static class ReflectionHelper
    {
        public static MethodInfo[] GetCustomImplementMethods(Type type)
        {
            return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        }

        public static MethodInfo[] GetCustomImplementMethods<T>() where T : class
        {
            return GetCustomImplementMethods(typeof(T));
        }

        public static MethodInfo? GetCustomImplementMethod(string methodName, Type type)
        {
            return GetCustomImplementMethods(type).FirstOrDefault(t => methodName.Equals(t.Name));
        }

        public static MethodInfo? GetCustomImplementMethod<T>(string methodName) where T : class
        {
            return GetCustomImplementMethod(methodName, typeof(T));
        }

        /// <summary>
        /// Types to check need to have same assembly as type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type[] GetTypesIsSubclassOf(Type type)
        {
            return type.Assembly.GetTypes().Where(t => t.IsSubclassOf(type)).ToArray();
        }

        /// <summary>
        /// Types to check need to have same assembly as type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type? GetTypeIsBaseOf(string name, Type type)
        {
            var testge = type.Assembly.GetTypes()[4];

            

            return type.Assembly.GetTypes().FirstOrDefault(t => name.Equals(t.Name) && type.IsEquivalentTo(t.BaseType));
        }

        public static Type? GetTypeIsSubclassOf(string name, Type type, Type typeAscendant)
        {
            return type.Assembly.GetTypes().FirstOrDefault(t => name.Equals(type.Name) && t.IsSubclassOf(typeAscendant));
        }

        public static object?[] GetObjectParameters(MethodInfo method, string[] paramValues)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                return Array.Empty<object?[]>();
            }

            object?[] objParams = new object?[parameters.Length];
            
            for (int i = 0; i < parameters.Length; i++)
            {
                objParams[i] = Convert.ChangeType(paramValues[i], parameters[i].ParameterType);
            }

            return objParams;
        }
    }
}