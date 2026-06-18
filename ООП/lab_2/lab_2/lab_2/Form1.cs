using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace lab_2_Airport
{
    public partial class listCrew : Form
    {
        private List<Plane> _planes = new List<Plane>();

        private readonly List<CrewMember> _currentCrew = new List<CrewMember>();
        public listCrew()
        {
            InitializeComponent();
        }
        private void btnAddCrewMember_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация ФИО
                if (string.IsNullOrWhiteSpace(txtCrewName.Text))
                    throw new Exception("Введите ФИО члена экипажа");

                var member = new CrewMember
                {
                    FullName = txtCrewName.Text,
                    Position = cmbCrewPost.SelectedItem?.ToString() ?? "Не указано",
                    Age = (int)numCrewAge.Value,
                    Experience = 5 
                };

                _currentCrew.Add(member);
                listBox1.Items.Add(member);

                txtCrewName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text)) throw new Exception("Введите ID самолета");

                var plane = new Plane
                {
                    ID = txtID.Text,
                    Model = cmbModel.Text,
                    Year = int.Parse(mtbYear.Text.Replace("_", "0")), 
                    PassengerSeats = (int)numSeats.Value,
                    Payload = trackPayload.Value,
                    LastMaintenance = dtpLastMaintenance.Value,

                    Type = rbPassenger.Checked ? "Пассажирский" : (rbCargo.Checked ? "Грузовой" : "Военный"),

                    Manufacturer = new Manufacturer
                    {
                        Name = txtManufName.Text,
                        Country = txtManufCountry.Text,
                        YearOfFoundation = (int)numManufYear.Value
                    },

                    Crew = new List<CrewMember>(_currentCrew)
                };

                _planes.Add(plane);
                listBox2.Items.Add($"{plane.ID} - {plane.Model}"); 

                _currentCrew.Clear();
                listBox1.Items.Clear();

                MessageBox.Show("Самолет успешно добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при создании");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Plane>));
            using (FileStream fs = new FileStream("planes.xml", FileMode.Create))
            {
                formatter.Serialize(fs, _planes);
                MessageBox.Show("Данные сохранены в planes.xml");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!File.Exists("planes.xml")) return;

            XmlSerializer formatter = new XmlSerializer(typeof(List<Plane>));
            using (FileStream fs = new FileStream("planes.xml", FileMode.Open))
            {
                _planes = (List<Plane>)formatter.Deserialize(fs);

                listBox2.Items.Clear();
                foreach (var p in _planes) listBox2.Items.Add($"{p.ID} - {p.Model}");

                MessageBox.Show("Данные загружены!");
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Сначала выберите самолет из списка ниже!");
                return;
            }

            int index = listBox2.SelectedIndex;
            var selectedPlane = _planes[index];

            double profit = selectedPlane.CalculateProfit();

            MessageBox.Show($"Прибыль для самолета {selectedPlane.Model} (ID: {selectedPlane.ID}): {profit}$", "Расчет прибыли");
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
        }
    }
}
