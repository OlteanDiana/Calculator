using CalculatorApp.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        INumberValidator _validator;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _validator = new IntNumberValidator(textBoxFirstNumber.Text);
            if (!_validator.Validate())
            {
                MessageBox.Show("First number is not valid.");
                return;
            }

            _validator = new IntNumberValidator(textBoxSecondNumber.Text);
            if (!_validator.Validate())
            {
                MessageBox.Show("Second number is not valid.");
                return;
            }

            try
            {
                IOperator firstOperator = new IntOperator(textBoxFirstNumber.Text);
                IOperator secondOperator = new IntOperator(textBoxSecondNumber.Text);

                IOperation add = new AddOperation(firstOperator, secondOperator);
                ICalculator calculator = new IntCalculator(add);

                textBoxResult.Text = calculator.Add();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
