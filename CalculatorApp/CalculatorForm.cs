using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private string _firstFilePath;
        private string _secondFilePath;
        private bool _filesImported;
        private AddHandler _addHandler;

        public CalculatorForm()
        {
            InitializeComponent();
            _addHandler = new AddHandler();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (openFileDialog.FileNames.Length != 2)
            {
                MessageBox.Show("Please select two text files!");
                return;
            }

            _firstFilePath = openFileDialog.FileNames[0];
            _secondFilePath = openFileDialog.FileNames[1];
            _filesImported = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!_filesImported)
            {
                MessageBox.Show("Please import files first.");
                return;
            }

            try
            {
                textBoxResult.Text = _addHandler.Add(_firstFilePath, _secondFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
