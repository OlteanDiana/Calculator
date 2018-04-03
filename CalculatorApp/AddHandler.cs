using System.Collections.Generic;

namespace CalculatorApp
{
    public class AddHandler
    {
        private FileUtils _utils;

        public AddHandler()
        {
            _utils = new FileUtils();
        }

        internal string Add(string _firstFilePath, string _secondFilePath)
        {
            char firstNumberSign, secondNumberSign;
            Stack<byte> firstNumber = _utils.ReadFromFile(_firstFilePath, out firstNumberSign);
            Stack<byte> secondNumber = _utils.ReadFromFile(_secondFilePath, out secondNumberSign);
            Stack<byte> result;

            if (firstNumberSign.Equals('+') && secondNumberSign.Equals('+'))
            {
                result = AddPositiveNumbers(firstNumber, secondNumber);
                return GetResultAsString(result);
            }

            return "Not implemented yet.";
        }

        private Stack<byte> AddPositiveNumbers(Stack<byte> firstNumber, Stack<byte> secondNumber)
        {
            Stack<byte> result = new Stack<byte>();
            byte carry = 0, firstNumberDigit, secondNumberDigit, currentAddValue;

            while (firstNumber.Count != 0 && secondNumber.Count != 0)
            {
                firstNumberDigit = firstNumber.Pop();
                secondNumberDigit = secondNumber.Pop();

                currentAddValue = (byte)(firstNumberDigit + secondNumberDigit + carry);
                result.Push((byte)(currentAddValue % 10));

                carry = (byte)(currentAddValue / 10);
            }

            if (firstNumber.Count > 0)
            {
                FinishAddOperation(firstNumber, carry, ref result);
                return result;
            }

            if (secondNumber.Count > 0)
            {
                FinishAddOperation(secondNumber, carry, ref result);
                return result;
            }

            return result;
        }

        private void FinishAddOperation(Stack<byte> firstNumber, byte carry, ref Stack<byte> result)
        {
            byte currentDigit, currentAddValue;

            while (firstNumber.Count != 0)
            {
                currentDigit = firstNumber.Pop();

                currentAddValue = (byte)(currentDigit + carry);
                result.Push((byte)(currentAddValue % 10));

                carry = (byte)(currentAddValue / 10);
            }
        }

        private string GetResultAsString(Stack<byte> result)
        {
            string resultString = "+";

            while (result.Count != 0)
            {
                resultString += result.Pop().ToString();
            }

            return resultString;
        }
    }
}
