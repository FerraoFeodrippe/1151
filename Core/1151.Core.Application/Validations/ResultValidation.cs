
namespace _1151.Core.Application.Validations
{
    public class ResultValidation
    {
        public bool IsOk { get => !Errors.Any(); }
        public string[] Errors { get; private set; } 

        public ResultValidation()
        {
            Errors = Array.Empty<string>();
        }

        public ResultValidation(string[] errors)
        {
            Errors = errors;
        }

        public ResultValidation ConcatResult(ResultValidation resultValidation)
        {
            Errors = Errors.Concat(resultValidation.Errors).ToArray();

            return this;
        }
    }
}
