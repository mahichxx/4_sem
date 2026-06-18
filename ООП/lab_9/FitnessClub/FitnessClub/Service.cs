using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub
{
    public class Service
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Description { get; set; } = "";

        // Для совместимости с твоим UI оставляем список
        [NotMapped] // Это поле не будет создаваться в таблице БД как колонка
        public List<string> Images { get; set; } = new List<string> { "" };

        // Это поле для хранения пути к главной картинке в БД
        public string ImagePath { get; set; } = "";

        public string Category { get; set; } = ""; // Оставляем строку для старого кода
        public double Rating { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public bool InStock { get; set; } = true;
        public int Quantity { get; set; }

        public string Color { get; set; } = "";
        public string Size { get; set; } = "";

        [NotMapped]
        public List<string> RelatedServices { get; set; } = new();

        public int PurchaseCount { get; set; }
        public string Country { get; set; } = "";
        public string Manufacturer { get; set; } = "";

        // Связь для Entity Framework (Пункт 3 лабы)
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? CategoryRef { get; set; }
    }
}