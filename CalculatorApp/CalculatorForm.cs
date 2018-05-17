using CalculatorApp.Interfaces;
using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private const string ADD = "add";
        private const string SUBSTRACT = "substract";

        INumberValidator _validator;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            PerformOperation(ADD);
        }

        private void buttonSubstract_Click(object sender, EventArgs e)
        {
            PerformOperation(SUBSTRACT);
        }

        private void PerformOperation(string operation)
        {
            textBoxResult.Text = string.Empty;

            if (!ValidateNumbers())
            {
                return;
            }

            try
            {
                IOperand<int> firstOperand = new IntOperand(textBoxFirstNumber.Text);
                IOperand<int> secondOperand = new IntOperand(textBoxSecondNumber.Text);

                ICalculator<int> calculator = new IntCalculator(firstOperand, secondOperand);

                switch (operation)
                {
                    case ADD:
                        {
                            textBoxResult.Text = calculator.Add();
                            break;
                        }
                    case SUBSTRACT:
                        {
                            textBoxResult.Text = calculator.Substract();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateNumbers()
        {
            _validator = new IntNumberValidator(textBoxFirstNumber.Text);
            if (!_validator.Validate())
            {
                MessageBox.Show("First number is not valid.");
                return false;
            }

            _validator = new IntNumberValidator(textBoxSecondNumber.Text);
            if (!_validator.Validate())
            {
                MessageBox.Show("Second number is not valid.");
                return false;
            }

            return true;
        }
    }
}
