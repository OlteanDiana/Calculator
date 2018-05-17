using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp
{
    public static class Extensions
    {
        public static Stack<int> GetStringAsIntStack(this string number)
        {
            Stack<int> result = new Stack<int>();
            char currentChar;
            int index = 0;

            while (index < number.Length)
            {
                currentChar = number[index];

                if (currentChar.Equals('+') || currentChar.Equals('-'))
                {
                    index++;
                    continue;
                }

                result.Push(int.Parse(currentChar.ToString()));
                index++;
            }

            return result;
        }
    }
}
