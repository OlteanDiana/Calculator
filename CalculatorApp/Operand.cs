using CalculatorApp.Interfaces;
using System.Linq;

namespace CalculatorApp
{
    internal class IntOperand : IOperand<int>
    {
        private string _operand;

        public IntOperand(string number)
        {
            _operand = number;
        }

        public string GetOperandAsString()
        {
            return _operand;
        }

        public char GetSign()
        {
            return _operand.First().Equals('+') || _operand.First().Equals('-') 
                   ? _operand.First() : '+';
        }
    }
}