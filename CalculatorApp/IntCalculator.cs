using CalculatorApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CalculatorApp
{
    public class IntCalculator : ICalculator<int>
    {
        #region Fields

        private IOperand<int> _firstOperand;
        private IOperand<int> _secondOperand;

        #endregion

        #region Constructor

        public IntCalculator(IOperand<int> firstOperand, IOperand<int> secondOperand)
        {
            _firstOperand = firstOperand;
            _secondOperand = secondOperand;
        } 

        #endregion

        #region InterfaceImplementation

        public string Add()
        {
            int compareResult = Compare();
            int checkSignResult = CompareSigns(_firstOperand.GetSign(), _secondOperand.GetSign());

            if (compareResult == 0 && checkSignResult != 0)
            {
                return "0";
            }

            if (checkSignResult == 0)
            {
                return ComputeAddResult(1);
            }

            if (compareResult > 0)
            {
                return ComputeDiffResult(1);
            }

            return ComputeDiffResult(1);
        }

        public string Substract()
        {
            int compareResult = Compare();
            int checkSignResult = CompareSigns(_firstOperand.GetSign(), _secondOperand.GetSign());

            if (checkSignResult == 0 && compareResult == 0)
            {
                return "0";
            }

            if (checkSignResult == 0 && compareResult > 0)
            {
                return ComputeDiffResult(1);
            }

            if (checkSignResult == 0 && compareResult < 0)
            {
                return ComputeDiffResult(2);
            }

            if (checkSignResult > 0)
            {
                return ComputeAddResult();
            }

            return ComputeAddResult(2);
        }

        #endregion

        #region CommonMethods

        private string ComputeDiffResult(int signOperandOrder)
        {
            string sign = GetSign(signOperandOrder);
            List<int> result = new List<int>();

            if (signOperandOrder == 1)
            {
                result = SubstractNumbers(_firstOperand.GetOperandWithoutSign()
                                                       .GetStringAsIntList(),
                                          _secondOperand.GetOperandWithoutSign()
                                                        .GetStringAsIntList());
            }
            else
            {
                result = SubstractNumbers(_secondOperand.GetOperandWithoutSign()
                                                        .GetStringAsIntList(),
                                           _firstOperand.GetOperandWithoutSign()
                                                        .GetStringAsIntList());
            }

            return string.Format("{0}{1}", sign, string.Join(string.Empty, result));
        }

        private string ComputeAddResult(int signOperandOrder = 0)
        {
            string sign = GetSign(signOperandOrder);
            Stack<int> result = AddNumbers(_firstOperand.GetOperand().GetStringAsIntStack(),
                                           _secondOperand.GetOperand().GetStringAsIntStack());

            return string.Format("{0}{1}", sign, string.Join(string.Empty, result.ToArray()));
        }

        private string GetSign(int signOperandOrder)
        {
            switch (signOperandOrder)
            {
                case 1:
                    {
                        return _firstOperand.GetSign().Equals('-') ? "-" : string.Empty;
                    }
                case 2:
                    {
                        return _secondOperand.GetSign().Equals('+') ? "-" : string.Empty;
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        } 

        #endregion

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
            string firstNumber = _firstOperand.GetOperandWithoutSign();
            string secondNumber = _secondOperand.GetOperandWithoutSign();
            int lengthCompare = firstNumber.Length
                                .CompareTo(secondNumber.Length);
            if (lengthCompare != 0)
            {
                return lengthCompare;
            }

            int index = 0;
            bool isEqual = false;

            while (index < firstNumber.Length)
            {
                isEqual = false;

                if (firstNumber[index].Equals(secondNumber[index]))
                {
                    isEqual = true;
                    index++;
                    continue;
                }

                break;
            }

            return isEqual ? 0 : firstNumber[index].CompareTo(secondNumber[index]);
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

        #region SubstractHandlers

        /// <summary>
        /// first number is always bigger
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        private List<int> SubstractNumbers(List<int> firstNumber, List<int> secondNumber)
        {
            List<int> result = new List<int>();
            int firstNumberDigit, secondNumberDigit, index1, index2, loan = 0, currentDigit;
            index1 = firstNumber.Count - 1;
            index2 = secondNumber.Count - 1;

            while (index2 >= 0)
            {
                firstNumberDigit = firstNumber[index1];
                secondNumberDigit = secondNumber[index2];

                if (firstNumberDigit >= secondNumberDigit && loan == 0)
                {
                    currentDigit = firstNumberDigit - secondNumberDigit;

                    if (currentDigit == 0 && index2 != 0)
                    {
                        result.Add(currentDigit);
                    }
                    else if (currentDigit != 0)
                    {
                        result.Add(currentDigit);
                    }

                    index1--;
                    index2--;
                    continue;
                }

                if (firstNumberDigit >= secondNumberDigit + loan)
                {
                    currentDigit = firstNumberDigit - secondNumberDigit - loan;

                    if (currentDigit == 0 && index2 != 0)
                    {
                        result.Add(currentDigit);
                    }
                    else if (currentDigit != 0)
                    {
                        result.Add(currentDigit);
                    }

                    index1--;
                    index2--;
                    loan = 0;
                    continue;
                }

                if (loan == 0)
                {
                    loan++;
                    currentDigit = (firstNumberDigit + 10) - secondNumberDigit;

                    if (currentDigit == 0 && index2 != 0)
                    {
                        result.Add(currentDigit);
                    }
                    else if (currentDigit != 0)
                    {
                        result.Add(currentDigit);
                    }

                    index1--;
                    index2--;
                    continue;
                }

                currentDigit = (firstNumberDigit - loan + 10) - secondNumberDigit;

                if (currentDigit == 0 && index2 != 0)
                {
                    result.Add(currentDigit);
                }
                else if (currentDigit != 0)
                {
                    result.Add(currentDigit);
                }

                index1--;
                index2--;
            }

            int remainingIndex;
            remainingIndex = firstNumber.Count - secondNumber.Count - 1;

            while (remainingIndex >= 0)
            {
                firstNumberDigit = firstNumber[remainingIndex];

                if (loan > 0)
                {
                    currentDigit = firstNumberDigit - loan;
                    loan = 0;
                }
                else
                {
                    currentDigit = firstNumberDigit;
                }

                if (currentDigit > 0 || (currentDigit == 0 && remainingIndex > 0))
                {
                    result.Add(currentDigit);
                }

                remainingIndex--;
            }

            return result.AsEnumerable().Reverse().ToList();
        } 

        #endregion
    }
}
