using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
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
            slDateTime.Text = DateTime.Now.ToLongTimeString();
        }

        private bool ValidateObject(object obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                MessageBox.Show(results[0].ErrorMessage, "Ошибка валидации");
                return false;
            }
            return true;
        }

        private void btnAddCrewMember_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new CrewMember
                {
                    FullName = txtCrewName.Text,
                    Position = cmbCrewPost.SelectedItem?.ToString(),
                    Age = (int)numCrewAge.Value,
                    Experience = 5
                };
                if (!ValidateObject(member)) return;
                _currentCrew.Add(member);
                listBox1.Items.Add(member);
                txtCrewName.Clear();
                slLastAction.Text = "Последнее действие: Добавлен член экипажа";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var plane = new Plane
                {
                    ID = txtID.Text,
                    Model = cmbModel.Text,
                    Year = int.TryParse(mtbYear.Text.Replace("_", ""), out int y) ? y : 0,
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

                if (!ValidateObject(plane)) return;

                _planes.Add(plane);
                UpdateMainList(_planes);
                _currentCrew.Clear();
                listBox1.Items.Clear();
                slLastAction.Text = "Последнее действие: Добавлен самолет " + plane.ID;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveToFile(_planes, "planes.xml");
            slLastAction.Text = "Последнее действие: Данные сохранены";
        }

        private void SaveToFile(List<Plane> data, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Plane>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, data);
                MessageBox.Show($"Данные сохранены в {filename}");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!File.Exists("planes.xml")) return;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Plane>));
            using (FileStream fs = new FileStream("planes.xml", FileMode.Open))
            {
                _planes = (List<Plane>)formatter.Deserialize(fs);
                UpdateMainList(_planes);
                MessageBox.Show("Данные загружены!");
            }
            slLastAction.Text = "Последнее действие: Данные загружены";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1) return;
            var selectedPlane = _planes[listBox2.SelectedIndex];
            MessageBox.Show($"Прибыль: {selectedPlane.CalculateProfit()}$");
            slLastAction.Text = "Последнее действие: Расчет прибыли";
        }

        private void UpdateMainList(List<Plane> list)
        {
            listBox2.Items.Clear();
            foreach (var p in list) listBox2.Items.Add($"{p.ID} - {p.Model} ({p.Year})");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            slDateTime.Text = DateTime.Now.ToLongTimeString();
            slCount.Text = "Объектов в базе: " + _planes.Count;
        }

        private void tsSearch_Click(object sender, EventArgs e)
        {
            SearchForm sf = new SearchForm(_planes);
            sf.Show();
            slLastAction.Text = "Последнее действие: Открыт поиск";
        }

        private void tsSort_Click(object sender, EventArgs e)
        {
            _planes = _planes.OrderBy(p => p.Year).ToList();
            UpdateMainList(_planes);
            slLastAction.Text = "Последнее действие: Быстрая сортировка по году";
        }

        private void sortByModel_Click(object sender, EventArgs e)
        {
            _planes = _planes.OrderBy(p => p.Model).ToList(); 
            UpdateMainList(_planes);
            slLastAction.Text = "Последнее действие: Сортировка по модели";
        }

        private void sortByYear_Click(object sender, EventArgs e)
        {
            _planes = _planes.OrderBy(p => p.Year).ToList();
            UpdateMainList(_planes);
            slLastAction.Text = "Последнее действие: Сортировка по году";
        }
        private void tsClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            mtbYear.Clear();
            cmbModel.SelectedIndex = -1;
            slLastAction.Text = "Последнее действие: Очистка полей";
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                _planes.RemoveAt(listBox2.SelectedIndex);
                UpdateMainList(_planes);
                slLastAction.Text = "Последнее действие: Удаление объекта";
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия 1.0\nРазработчик: Твое ФИО\n© 2024 Аэропорт Менеджер", "О программе");
        }

    }
}