using _1151.Cross.DepedencyInjection.Helpers;
using _1152.Cross.Util.Helpers;
using _1152.Modules.Implementation;

namespace _1151.Core.Application.Validations.ValidationsApp
{
    public static class MethodValidation
    {
        public static ResultValidation Validate(string[] args)
        {
            List<string> errors = new(1);

            var type = ReflectionHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

            if (type!= null)
            {
                var method = ReflectionHelper.GetCustomImplementMethod(args[1], type);

                if (method == null)
                {
                    errors.Add($"There is not such method {args[1]} for this module {args[0]}.");
                }
            }

            return new ResultValidation(errors.ToArray());
        }
    }
}
