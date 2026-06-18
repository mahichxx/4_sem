using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_Airport
{
    [Serializable]
    public class Manufacturer
    {
        [Required(ErrorMessage = "Название компании обязательно")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]+$", ErrorMessage = "Страна должна начинаться с большой буквы и содержать только буквы")]
        public string Country { get; set; }

        [Range(1800, 2024, ErrorMessage = "Год основания должен быть между 1800 и 2024")]
        public int YearOfFoundation { get; set; }

        public override string ToString() => $"{Name} ({Country})";
    }
}
