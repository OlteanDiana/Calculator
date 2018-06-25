using CalculatorApp.Interfaces;
using System.Text.RegularExpressions;

namespace CalculatorApp
{
    public class IntNumberValidator : INumberValidator
    {
        private string _number;

        public IntNumberValidator(string number)
        {
            _number = number;
        }

        public bool Validate()
        {
            return Regex.IsMatch(_number, "^0$") 
                   || Regex.IsMatch(_number, "^[-+]?[1-9][0-9]*$");
        }
    }
}