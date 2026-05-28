using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq; 
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using System.Xml.Serialization;

namespace lab_2_Airport
{
    public partial class SearchForm : Form
    {
        private List<Plane> _allPlanes; 
        private List<Plane> _lastResults = new List<Plane>(); 
        public SearchForm(List<Plane> planes)
        {
            InitializeComponent();
            _allPlanes = planes;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                dgvResults.Items.Clear();

                string pattern = txtSearchModel.Text.Trim();
                if (string.IsNullOrEmpty(pattern)) pattern = ".*";

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                var results = _allPlanes.Where(p =>
                    (regex.IsMatch(p.Model) || regex.IsMatch(p.ID)) &&
                    p.Year >= (int)numYearFrom.Value
                ).ToList();

                _lastResults = results;

                foreach (var plane in results)
                {
                    dgvResults.Items.Add($"{plane.ID} - {plane.Model} ({plane.Year})");
                }

                if (results.Count == 0) MessageBox.Show("Ничего не найдено");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            var results = dgvResults.Items;

            if (results.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<string>));
            using (FileStream fs = new FileStream("search_results.xml", FileMode.Create))
            {
                List<string> lines = new List<string>();
                foreach (var item in dgvResults.Items) lines.Add(item.ToString());
                

                formatter.Serialize(fs, lines);
                MessageBox.Show("Результаты поиска сохранены в search_results.xml");
            }

        }
        private void dgvResults_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_lastResults == null || _lastResults.Count == 0)
            {
                MessageBox.Show("Сначала найдите что-нибудь, чтобы сохранить!");
                return;
            }

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Plane>));
                using (FileStream fs = new FileStream("search_results.xml", FileMode.Create))
                {
                    formatter.Serialize(fs, _lastResults);
                    MessageBox.Show("Результаты поиска сохранены в файл search_results.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchModel.Clear();    
            numYearFrom.Value = 0;    
            dgvResults.Items.Clear();   
            _lastResults.Clear();       
        }
    }
}