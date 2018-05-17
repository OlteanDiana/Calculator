namespace CalculatorApp.Interfaces
{
    public interface IOperand<T>
    {
        char GetSign();
        string GetOperandAsString();
    }
}
