using System.Reflection;

namespace _1152.Modules.Contracts
{
    public interface IModule
    {
        string GetModuleName();
        string GetMethodName();
        string[] GetModuleMethodsName();
        ParameterInfo[] GetParameters();
        string[] GetParametersName();
        string[] GetParametersValues();
        void Run();    
    }
}