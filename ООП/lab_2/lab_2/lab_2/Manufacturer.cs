using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_Airport
{
    [Serializable]
    public class Manufacturer
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearOfFoundation { get; set; }
        public string PlaneTypes { get; set; } // Какие типы производит

        public override string ToString() => $"{Name} ({Country})";
    }
}