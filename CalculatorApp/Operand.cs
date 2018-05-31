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

        public string GetOperand()
        {
            return _operand;
        }

        public string GetOperandWithoutSign()
        {
            return _operand.First().Equals('+') || _operand.First().Equals('-')
                ? _operand.Substring(1) : _operand;
        }

        public char GetSign()
        {
            return _operand.First().Equals('+') || _operand.First().Equals('-') 
                   ? _operand.First() : '+';
        }
    }
}