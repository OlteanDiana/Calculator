using System.Collections.Generic;

namespace CalculatorApp
{
    public static class Utils
    {
        public static string GetNumberAsString(Stack<int> number)
        {
            return $"{string.Join(string.Empty, number.ToArray())}";
        }
    }
}
