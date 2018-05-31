using CalculatorApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            int compareResult = Compare();
            if (compareResult == 0)
            {
                return "0";
            }

            if (_firstOperand.GetSign().Equals(_secondOperand.GetSign()))
            {
                string sign = _firstOperand.GetSign().Equals('-') ? "-" : string.Empty;
                Stack<int>  result = AddNumbers(_firstOperand.GetOperand().GetStringAsIntStack(),
                            _secondOperand.GetOperand().GetStringAsIntStack());
                return sign + string.Join(string.Empty, result.ToArray());
            }

            return "Not implemented yet.";
        }

        public string Substract()
        {
            int compareResult = Compare();
            if (compareResult == 0)
            {
                return "0";
            }

            if (_firstOperand.GetSign().Equals('+')
                && _secondOperand.GetSign().Equals('-'))
            {
                Stack<int> result = AddNumbers(_firstOperand.GetOperand().GetStringAsIntStack(),
                            _secondOperand.GetOperand().GetStringAsIntStack());
                return string.Join(string.Empty, result.ToArray());
            }

            if (_firstOperand.GetSign().Equals('+')
                && _secondOperand.GetSign().Equals('+')
                && compareResult == 1)
            {
                List<int> result = SubstractNumbers(
                    _firstOperand.GetOperandWithoutSign().GetStringAsIntList(),
                    _secondOperand.GetOperandWithoutSign().GetStringAsIntList());
                return string.Join(string.Empty, result);
            }

            return "Not implemented yet.";
        }

        #region Comparers

        /// <summary>
        /// compares the sign to check which number is bigger
        /// </summary>
        /// <returns>
        /// 0, if the signs are equal
        /// 1, if the first number is bigger
        /// -1, if the second number is bigger
        /// </returns>
        private int CompareSigns(char firstNumberSign, char secondNumberSign)
        {
            if (firstNumberSign.Equals(secondNumberSign))
            {
                return 0;
            }

            if (!firstNumberSign.Equals(secondNumberSign)
                && firstNumberSign.Equals('+'))
            {
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// compares two large numbers
        /// </summary>
        /// <returns>
        /// 0, if they are equal
        /// 1, if the first number is bigger
        /// -1, if the second number is bigger
        /// </returns>
        private int Compare()
        {
            int checkSignResult = CompareSigns(_firstOperand.GetSign(), _secondOperand.GetSign());

            if (checkSignResult != 0)
            {
                return checkSignResult;
            }

            string firstNumber = _firstOperand.GetOperand();
            string secondNumber = _secondOperand.GetOperand();
            int lengthCompare = firstNumber.Length
                                .CompareTo(secondNumber.Length);
            if (lengthCompare != 0)
            {
                return lengthCompare;
            }

            int index = 0;
            while (index < firstNumber.Length)
            {
                if (firstNumber[index].Equals(secondNumber[index]))
                {
                    continue;
                }

                break;
            }

            return firstNumber[index].CompareTo(secondNumber[index]);
        } 

        #endregion

        #region AddHandlers

        private Stack<int> AddNumbers(Stack<int> firstNumber, Stack<int> secondNumber)
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

        #endregion

        /// <summary>
        /// first number is always bigger
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        private List<int> SubstractNumbers(List<int> firstNumber, List<int> secondNumber)
        {
            List<int> result = new List<int>();
            int firstNumberDigit, secondNumberDigit, index1, index2 , loan = 0;
            index1 = firstNumber.Count - 1;
            index2 = secondNumber.Count - 1;

            while (index2 >= 0)
            {
                firstNumberDigit = firstNumber[index1];
                secondNumberDigit = secondNumber[index2];

                if(firstNumberDigit >= secondNumberDigit && loan == 0)
                {
                    result.Add(firstNumberDigit - secondNumberDigit);
                    index1--;
                    index2--;
                    continue;
                }

                if (firstNumberDigit >= secondNumberDigit + loan)
                {
                    result.Add(firstNumberDigit - secondNumberDigit - loan);
                    index1--;
                    index2--;
                    loan = 0;
                    continue;
                }

                if(loan == 0)
                {
                    loan++;
                    result.Add((firstNumberDigit + 10) - secondNumberDigit);
                    index1--;
                    index2--;
                    continue;
                }

                result.Add((firstNumberDigit - loan + 10) - secondNumberDigit);
                index1--;
                index2--;
            }

            int remainingIndex, currentDigit;
            remainingIndex = firstNumber.Count - secondNumber.Count - 1;

            while (remainingIndex >= 0)
            {
                firstNumberDigit = firstNumber[remainingIndex];
                currentDigit = firstNumberDigit - loan;
                if (currentDigit > 0)
                {
                    result.Add(currentDigit);
                }

                remainingIndex--;
            }

            return result.AsEnumerable().Reverse().ToList();
        }
    }
}
