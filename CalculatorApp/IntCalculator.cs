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
        private ICustomComparer<string> _comparer;

        #endregion

        #region Constructor

        public IntCalculator(IOperand<int> firstOperand, IOperand<int> secondOperand)
        {
            _firstOperand = firstOperand;
            _secondOperand = secondOperand;
            _comparer = new IntComparer();
        } 

        #endregion

        #region InterfaceImplementation

        public string Add()
        {
            int compareResult = _comparer.Compare(_firstOperand.GetOperandWithoutSign(), 
                                                  _secondOperand.GetOperandWithoutSign());
            int checkSignResult = _comparer.CompareSigns(_firstOperand.GetSign().ToString(), 
                                                         _secondOperand.GetSign().ToString());

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

            return ComputeDiffResult(2);
        }

        public string Substract()
        {
            int compareResult = _comparer.Compare(_firstOperand.GetOperandWithoutSign(),
                                                  _secondOperand.GetOperandWithoutSign());
            int checkSignResult = _comparer.CompareSigns(_firstOperand.GetSign().ToString(),
                                                        _secondOperand.GetSign().ToString());

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

        public string Multiply()
        {
            char firstNumberSign = _firstOperand.GetSign();
            char secondNumberSign = _secondOperand.GetSign();
            string sign = !firstNumberSign.Equals(secondNumberSign) ? "-" : string.Empty;
            List<int> result = new List<int>();

            result = MultiplyNumbers(_firstOperand.GetOperandWithoutSign()
                                                       .GetStringAsIntStack(),
                                     _secondOperand.GetOperandWithoutSign()
                                                   .GetStringAsIntStack());

            return string.Format("{0}{1}", sign, string.Join(string.Empty, result));
        }

        public string Divide()
        {
            char firstNumberSign = _firstOperand.GetSign();
            char secondNumberSign = _secondOperand.GetSign();
            string sign = !firstNumberSign.Equals(secondNumberSign) ? "-" : string.Empty;

            string result = DivideNumbers(_firstOperand.GetOperandWithoutSign(), 
                _secondOperand.GetOperandWithoutSign());

            return string.Format("{0}{1}", sign, result);
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
                        return _secondOperand.GetSign().Equals('-') ? "-" : string.Empty;
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
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

                    if (currentDigit == 0 && index2 > 0)
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

        #region MultiplyHandlers

        private List<int> MultiplyNumbers(Stack<int> first, Stack<int> second)
        {
            int[,] matrix = new int[second.Count, first.Count + second.Count];
            int current = 0, carry = 0, rowIndex = 0, columnIndex = 0;

            foreach (int secondDigit in second)
            {
                foreach (int firstDigit in first)
                {
                    current = (firstDigit * secondDigit) + carry;
                    matrix[rowIndex, columnIndex] = current % 10;
                    columnIndex++;
                    carry = 0;

                    if (current / 10 > 0)
                    {
                        carry = current / 10;
                    }
                }

                if (carry > 0)
                {
                    matrix[rowIndex, columnIndex] = carry;
                    carry = 0;
                }

                rowIndex++;
                columnIndex = rowIndex;
            }

            return AddMatrixRows(matrix);
        }

        private List<int> AddMatrixRows(int[,] matrix)
        {
            List<int> result = new List<int>();
            int rows = matrix.GetUpperBound(0) - matrix.GetLowerBound(0) + 1;
            int cols = matrix.GetUpperBound(1) - matrix.GetLowerBound(1) + 1;
            int carry = 0, current;

            for (int j = 0; j < cols; j++)
            {
                current = 0;

                for (int i = 0; i < rows; i++)
                {
                    current += matrix[i, j];
                }

                current += carry;
                carry = current / 10;
                result.Add(current % 10);
            }

            return result.TrimZeros();
        }

        #endregion

        #region DivideHandlers

        private string DivideNumbers(string first, string second)
        {
            List<int> integerPart = new List<int>();
            List<int> decimalPart = new List<int>();

            bool continueDivision = true, isInteger = true;
            int indexFirst = GetStartIndex(first, second);
            string divident = first.Substring(0, indexFirst);

            while (continueDivision)
            {
                string value;
                int multiplyIndex = MultiplyUntilBigger(divident, second, out value);

                if (isInteger)
                {
                    isInteger = multiplyIndex != 0;
                    integerPart.Add(multiplyIndex);
                }
                else
                {
                    decimalPart.Add(multiplyIndex);
                }

                if ((_comparer.Compare(divident, value) == 0
                    && indexFirst == first.Length)
                    || decimalPart.Count > 10)
                {
                    continueDivision = false;
                    continue;
                }

                List<int> diff = SubstractNumbers(divident.GetStringAsIntList(),
                                                  value.GetStringAsIntList());
                bool hasDigitsLeft = indexFirst < first.Length - 1;
                if (hasDigitsLeft)
                {
                    divident = string.Join(string.Empty, diff) + first.ElementAt(indexFirst + 1);
                    indexFirst++;
                }
                else
                {
                    isInteger = false;
                    divident = string.Join(string.Empty, diff) + "0";
                }
            }

            if(decimalPart.Count == 0)
            {
                return string.Join(string.Empty, integerPart);
            }

            return string.Format("{0},{1}", string.Join(string.Empty, integerPart),
                string.Join(string.Empty, decimalPart));
        }

        private int GetStartIndex(string first, string second)
        {
            int index = 0;

            while (index < first.Length)
            {
                if (_comparer.Compare(first.Substring(0, index),
                    second) < 0)
                {
                    index++;
                    continue;
                }

                break;
            }
            
            return index;
        }

        private int MultiplyUntilBigger(string first, string second, out string value)
        {
            int multiplyIndex = 1;
            string currentValue = string.Empty;

            if(_comparer.Compare(first, second) < 0)
            {
                value = "0";
                return 0;
            }

            do
            {
                multiplyIndex++;
                value = currentValue;
                currentValue = string.Join(string.Empty, 
                                MultiplyNumbers(second.GetStringAsIntStack(), 
                                                multiplyIndex.ToString().GetStringAsIntStack()));
            } while (_comparer.Compare(first, currentValue) >= 0);

            return multiplyIndex - 1;
        }

        #endregion
    }
}
