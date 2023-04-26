# 1151
1151 - A CLI system designed to be easy to increment with just code. So anyone can use this as template and edit with your modifications.

This project works on create modules (since it is from BaseModule) inside _1152.Modules.Implementation that can be called anytime, as log it exists with its methods by just adding code and no more configuration. This one reads assembly to check all types on Implementation Project and read by reflection its methods to run.


# Run/Build Modules

Go on Presentation project and run (it builds for itself):

> dotnet run {modulename} {methodName} {parametersDefined}

If do not needs build run: 
> dotnet run --no-build {modulename} {methodName} {parametersDefined}

Or you can run on builded folder the dll "152.Presentation.Console.dll" since you are on root of this folder
-> dotnet 1152.Presentation.Console.dll BasicUtil Sum 1 1

After inside context you can type at start -stop to stop aplication
-> -stop

After inside context you can type at start -reset to reset module input
-> -reset

# Possible mapped errors

If less than 2 arguments are passed, it informs: 
> "Should have at last 2 values of args."

If passed a module and it do not exists, it informs:
> "There is not such module {modulename}."

If passed a method that there is not on module, it informs:
> "There is not such method {methodName} for this module {modulename}."

If passed a number of parameters less then method needs, it informs:

> "Need at last {number of parameters} arguments for method {methodName}.
