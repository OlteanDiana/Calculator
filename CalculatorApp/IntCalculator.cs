using CalculatorApp.Interfaces;

namespace CalculatorApp
{
    public class IntCalculator : ICalculator
    {
        private IOperation _addOperation;

        public IntCalculator(IOperation addOperation)
        {
            _addOperation = addOperation;
        }

        public string Add()
        {
            return _addOperation.PerformOperation();
        }
    }
}
