using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_Airport
{
    [Serializable]
    public class Plane
    {
        public string ID { get; set; }
        public string Type { get; set; } 
        public string Model { get; set; }
        public int PassengerSeats { get; set; }
        public int Year { get; set; }
        public double Payload { get; set; } // Грузоподъемность
        public DateTime LastMaintenance { get; set; } // Последнее ТО
        public Manufacturer Manufacturer { get; set; } = new Manufacturer();
        public List<CrewMember> Crew { get; set; } = new List<CrewMember>();
        public double CalculateProfit()
        {
            // (места * 100) + (грузоподъемность * 50)
            return (PassengerSeats * 100) + (Payload * 50);
        }
    }
}
