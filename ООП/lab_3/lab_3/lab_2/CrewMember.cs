using System;
using System.ComponentModel.DataAnnotations; // Библиотека для атрибутов

namespace lab_2_Airport
{
    [Serializable]
    public class CrewMember
    {
        [Required(ErrorMessage = "ФИО обязательно")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Должность обязательна")]
        public string Position { get; set; }

        [Range(18, 70, ErrorMessage = "Возраст экипажа должен быть от 18 до 70 лет")]
        public int Age { get; set; }

        // Это свойство стажа (можно добавить атрибут и сюда)
        public int Experience { get; set; }

        public override string ToString() => $"{FullName} - {Position}";
    }
}