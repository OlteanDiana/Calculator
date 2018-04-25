using System.Collections.Generic;

namespace CalculatorApp.Interfaces
{
    public interface IOperator
    {
        string Operator { get; set; }
        char GetSign();
        Stack<int> GetOperatorAsStack();
    }
}
