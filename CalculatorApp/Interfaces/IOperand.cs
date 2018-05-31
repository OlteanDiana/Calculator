namespace CalculatorApp.Interfaces
{
    public interface IOperand<T>
    {
        char GetSign();
        string GetOperand();
        string GetOperandWithoutSign();
    }
}
