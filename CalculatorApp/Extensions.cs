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

        public static List<int> GetStringAsIntList(this string number)
        {
            List<int> result = new List<int>();
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

                result.Add(int.Parse(currentChar.ToString()));
                index++;
            }

            return result;
        }
    }
}
