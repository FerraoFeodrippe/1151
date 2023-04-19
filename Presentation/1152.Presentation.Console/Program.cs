// See https://aka.ms/new-console-template for more information
using _1152.Modules.Application.BasicUtil;

Console.WriteLine("Hello, World!");


var x = new BasicUtilModule(args);

Console.WriteLine(x.Name);
Console.WriteLine(x.MethodName);

foreach(var method in x.MethodsName)
{
    Console.WriteLine(method);
}

foreach (var parameter in x.ParametersName)
{
    Console.WriteLine(parameter);
}


Console.WriteLine();
