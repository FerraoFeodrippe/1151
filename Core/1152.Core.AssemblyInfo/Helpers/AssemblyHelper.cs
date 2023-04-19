using System.Reflection;

namespace _1152.Core.AssemblyInfo.Helpers
{
    public static class AssemblyHelper
    {
        public static MethodInfo[] CustomImplementMethods(Type type)
        {
            return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        }

        public static MethodInfo[] CustomImplementMethods<T>() where T : class
        {
            return CustomImplementMethods(typeof(T));
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
    }
}