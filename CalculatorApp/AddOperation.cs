using CalculatorApp.Interfaces;
using System.Collections.Generic;

namespace CalculatorApp
{
    public class AddOperation : IOperation
    {
        public IOperator FirstOperator { get; set; }
        public IOperator SecondOperator { get; set; }

        public AddOperation(IOperator firstOperator, IOperator secondOperator)
        {
            FirstOperator = firstOperator;
            SecondOperator = secondOperator;
        }

        public string PerformOperation()
        {
            if (FirstOperator.GetSign().Equals(SecondOperator.GetSign()))
            {
                return FirstOperator.GetSign()
                       + Utils.GetNumberAsString(
                                    AddPositiveNumbers(FirstOperator.GetOperatorAsStack(),
                                                       SecondOperator.GetOperatorAsStack()));
            }

            return "Not implemented yet.";
        }

        private Stack<int> AddPositiveNumbers(Stack<int> firstNumber, Stack<int> secondNumber)
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
