namespace CalculatorApp.Interfaces
{
    public interface IOperation
    {
        IOperator FirstOperator { get; set; }
        IOperator SecondOperator { get; set; }

        string PerformOperation();
    }
}
