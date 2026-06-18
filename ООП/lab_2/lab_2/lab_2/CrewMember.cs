using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_Airport
{
    [Serializable]
    public class CrewMember
    {
        public string FullName { get; set; }
        public string Position { get; set; } // Пилот, стюардесса и т.д.
        public int Age { get; set; }
        public int Experience { get; set; }

        public override string ToString() => $"{FullName} - {Position}";
    }
}
