using CalculatorApp.Interfaces;

namespace CalculatorApp
{
    public class IntComparer : ICustomComparer<string>
    {
        #region Constructor

        public IntComparer()
        {
        }

        #endregion

        #region InterfaceImplementation

        /// <summary>
        /// compares two large numbers
        /// </summary>
        /// <returns>
        /// 0, if they are equal
        /// 1, if the first number is bigger
        /// -1, if the second number is bigger
        /// </returns>
        public int Compare(string firstNumber, string secondNumber)
        {
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

        /// <summary>
        /// compares the sign to check which number is bigger
        /// </summary>
        /// <returns>
        /// 0, if the signs are equal
        /// 1, if the first number is bigger
        /// -1, if the second number is bigger
        /// </returns>
        public int CompareSigns(string firstNumberSign, string secondNumberSign)
        {
            if (firstNumberSign.Equals(secondNumberSign))
            {
                return 0;
            }

            if (!firstNumberSign.Equals(secondNumberSign)
                && firstNumberSign.Equals("+"))
            {
                return 1;
            }

            return -1;
        } 

        #endregion
    }
}
