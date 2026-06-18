using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_1
{
    public partial class Form1 : Form
    {
        private UnitCalculator _calculator = new UnitCalculator();

        public Form1()
        {
            InitializeComponent();
            _calculator.OnCalculationFinished += DisplayResult;
        }
        private void DisplayResult(double result)
        {
            txtOutput.Text = result.ToString("F4"); 
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFrom.Items.Clear();
            cmbTo.Items.Clear();

            string selected = cmbCategory.SelectedItem.ToString().Trim();

            if (selected == "Длина")
            {
                string[] units = { "Метры", "Футы", "Дюймы" };
                cmbFrom.Items.AddRange(units);
                cmbTo.Items.AddRange(units);
            }
            else if (selected == "Вес")
            {
                string[] units = { "Килограммы", "Фунты" };
                cmbFrom.Items.AddRange(units);
                cmbTo.Items.AddRange(units);
            }
            else if (selected == "Объем")
            {
                string[] units = { "Литры", "Галлоны" };
                cmbFrom.Items.AddRange(units);
                cmbTo.Items.AddRange(units);
            }

            if (cmbFrom.Items.Count > 0) cmbFrom.SelectedIndex = 0;
            if (cmbTo.Items.Count > 0) cmbTo.SelectedIndex = 0;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFrom.SelectedItem == null || cmbTo.SelectedItem == null)
                {
                    throw new Exception("Пожалуйста, выберите обе единицы измерения (Из и В).");
                }

                double value = double.Parse(txtInput.Text);

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Значение не может быть отрицательным для мер длины, веса или объема.");
                }

                _calculator.Convert(value, cmbFrom.SelectedItem.ToString(), cmbTo.SelectedItem.ToString());
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Введите корректное число (используйте запятую для дробей).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInput.Focus(); 
            }
            catch (OverflowException)
            {
                MessageBox.Show("Ошибка: Число слишком большое.", "Ошибка диапазона", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Некорректное значение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("Попытка конвертации завершена.");
            }
        }
    }
}
