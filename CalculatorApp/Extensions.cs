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

        public static int[] GetRow(this int[,] matrix, int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new int[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }

        public static List<int> TrimZeros(this List<int> array)
        {
            List<int> reverse = array.AsEnumerable().Reverse().ToList();
            List<int> trimed = new List<int>();
            bool stopTrim = false;

            foreach(int i in reverse)
            {
                if (!stopTrim && i == 0)
                {
                    continue;
                }

                stopTrim = true;
                trimed.Add(i);
            }

            return trimed;
        }
    }
}
