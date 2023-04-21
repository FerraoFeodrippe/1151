using _1152.Cross.Util.Helpers;
using _1152.Modules.Implementation;

namespace _1151.Core.Application.Validations.ValidationsApp
{
    public static class ModuleValidation
    {
        public static ResultValidation Validate(string[] args)
        {
            List<string> errors = new(2);

            var type = ReflectionHelper.GetTypeIsBaseOf($"{args[0]}{BaseModule.ModuleSufix}", typeof(BaseModule));

            if (type == null)
            {
                errors.Add($"There is not such module {args[0]}.");
            }

            return new ResultValidation(errors.ToArray());
        }
    }
}
