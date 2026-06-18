using lab_2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab_2_Airport
{
    [Serializable] 
    public class Plane
    {
        [Required(ErrorMessage = "ID обязателен")]
        [RegularExpression(@"^[A-Z]{2}-\d{4}$", ErrorMessage = "Формат ID: AA-1234")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Укажите модель")]
        public string Model { get; set; }

        [YearValidation(ErrorMessage = "Год выпуска не может быть из будущего")] 
        public int Year { get; set; }

        public DateTime LastMaintenance { get; set; }

        public string Type { get; set; }

        [Range(1, 800, ErrorMessage = "Мест должно быть от 1 до 800")] 
        public int PassengerSeats { get; set; }

        [Range(20, 200)]
        public double Payload { get; set; }

        public Manufacturer Manufacturer { get; set; } = new Manufacturer();
        public List<CrewMember> Crew { get; set; } = new List<CrewMember>();

        public double CalculateProfit()
        {
            return (PassengerSeats * 100) + (Payload * 50);
        }
    }
}