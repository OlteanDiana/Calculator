using CalculatorApp.Interfaces;
using System.Collections.Generic;

namespace CalculatorApp
{
    public class IntCalculator : ICalculator<int>
    {
        private IOperand<int> _firstOperand;
        private IOperand<int> _secondOperand;

        public IntCalculator(IOperand<int> firstOperand, IOperand<int> secondOperand)
        {
            _firstOperand = firstOperand;
            _secondOperand = secondOperand;
        }

        public string Add()
        {
            Stack<int> result;
            string sign;

            if (_firstOperand.GetSign().Equals(_secondOperand.GetSign()))
            {
                sign = _firstOperand.GetSign().Equals('-') ? "-" : string.Empty;
                result = AddNumbersWithSameSign(
                            _firstOperand.GetOperandAsString().GetStringAsIntStack(),
                            _secondOperand.GetOperandAsString().GetStringAsIntStack());
                return sign + string.Join(string.Empty, result.ToArray());
            }

            return "Not implemented yet.";
        }

        public string Substract()
        {
            Stack<int> result;
            string sign;

            if (_firstOperand.GetSign().Equals('+')
                && _secondOperand.GetSign().Equals('-'))
            {
                result = AddNumbersWithSameSign(
                            _firstOperand.GetOperandAsString().GetStringAsIntStack(),
                            _secondOperand.GetOperandAsString().GetStringAsIntStack());
                return string.Join(string.Empty, result.ToArray());
            }


            return "Not implemented yet.";
        }

        private Stack<int> AddNumbersWithSameSign(Stack<int> firstNumber, Stack<int> secondNumber)
        {
            Stack<int> result = new Stack<int>();
            int carry = 0, firstNumberDigit, secondNumberDigit, currentAddValue;

            while (firstNumber.Count != 0 && secondNumber.Count != 0)
            {
                firstNumberDigit = firstNumber.Pop();
                secondNumberDigit = secondNumber.Pop();

                currentAddValue = firstNumberDigit + secondNumberDigit + carry;
                result.Push(currentAddValue % 10);

                carry = currentAddValue / 10;
            }

            if (firstNumber.Count == 0 && secondNumber.Count == 0 && carry > 0)
            {
                result.Push(carry);
                return result;
            }

            if (firstNumber.Count > 0)
            {
                return FinishAddOperation(firstNumber, carry, result);
            }

            if (secondNumber.Count > 0)
            {
                return FinishAddOperation(secondNumber, carry, result);
            }

            return result;
        }

        private Stack<int> FinishAddOperation(Stack<int> number, int carry, Stack<int> result)
        {
            int currentDigit, currentAddValue;

            while (number.Count != 0)
            {
                currentDigit = number.Pop();

                currentAddValue = currentDigit + carry;
                result.Push(currentAddValue % 10);

                carry = currentAddValue / 10;
            }

            if (carry > 0)
            {
                result.Push(carry);
            }

            return result;
        }
    }
}
