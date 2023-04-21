using _1151.Core.Application.Validations.ValidationsApp;

namespace _1151.Core.Application.Validations
{
    public static class ValidationManager
    {
        public static ResultValidation Start(string[] args)
        {
            ResultValidation result = new();

            if (args.Length > 0)
            {
                result.ConcatResult(ModuleValidation.Validate(args));
            }

            if (args.Length < 2)
            {
                result.ConcatResult(new ResultValidation(new string[] { "Should have at last 2 values of args." }));
            }
            else
            {
                result = result.ConcatResult(MethodValidation.Validate(args))
                     .ConcatResult(ParametersValidation.Validate(args));
            }

            return result;
        }
    }
}
