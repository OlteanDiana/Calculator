using CalculatorApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp
{
    internal class IntOperator : IOperator
    {
        public string Operator { get; set; }

        public IntOperator(string number)
        {
            Operator = number;
        }

        public char GetSign()
        {
            return Operator.First().Equals('+') || Operator.First().Equals('-') 
                   ? Operator.First() : '+';
        }

        public Stack<int> GetOperatorAsStack()
        {
            Stack<int> result = new Stack<int>();
            char currentChar;
            int index = 0;

            while (index < Operator.Length)
            {
                currentChar = Operator[index];

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