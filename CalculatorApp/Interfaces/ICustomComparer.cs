using System.Collections.Generic;

namespace CalculatorApp.Interfaces
{
    interface ICustomComparer<T> : IComparer<T> 
    {
        int CompareSigns(T firstNumberSign, T secondNumberSign);
    }
}
