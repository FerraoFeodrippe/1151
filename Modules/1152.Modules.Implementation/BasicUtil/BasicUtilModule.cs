namespace _1152.Modules.Implementation.BasicUtil
{
    public class BasicUtilModule: BaseModule
    {
        public BasicUtilModule(string[] args) : base(args)
        {
        }

        public DateTime GetActualTime()
        {
            return DateTime.Now;
        }

        public decimal Sum(decimal number1, decimal number2)
        {
            return number1 + number2;
        }

        public decimal Sub(decimal number1, decimal number2)
        {
            return number1 - number2;
        }

        public decimal Mult(decimal number1, decimal number2)
        {
            return number1 * number2;
        }

        public decimal Div(decimal number1, decimal number2)
        {
            return number1 / number2;
        }
    }
}
