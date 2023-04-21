using _1152.Cross.Util.Helpers;
using _1152.Modules.Implementation;

namespace _1151.Core.Application.Validations.ValidationsApp
{
    public static class ParametersValidation
    {
        public static ResultValidation Validate(string[] args)
        {
            List<string> errors = new(1);

            var type = ReflectionHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

            if (type != null)
            {
                var method = ReflectionHelper.GetCustomImplementMethod(args[1], type);

                if (method?.GetParameters().Length > args.Length - 2)
                {
                    errors.Add($"Need at last {method.GetParameters().Length} arguments for method {args[1]}.");
                }
            }

            return new ResultValidation(errors.ToArray());
        }
    }
}
