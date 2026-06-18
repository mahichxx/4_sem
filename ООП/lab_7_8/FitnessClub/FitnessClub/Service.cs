using System.Collections.Generic;

namespace FitnessClub
{
    public class Service
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = ""; //краткое название
        public string FullName { get; set; } = ""; //полное название
        public string Description { get; set; } = ""; //описание
        public List<string> Images { get; set; } = new List<string> { "" };
        public string Category { get; set; } = "";//категория
        public double Rating { get; set; } //рейтинг
        public decimal Price { get; set; } //цена
        public int DiscountPercent { get; set; } //скидка
        public bool InStock { get; set; } = true; //нет в наличии
        public int Quantity { get; set; } //количество
        public string Color { get; set; } = ""; //цвет
        public string Size { get; set; } = ""; //размер
        public List<string> RelatedServices { get; set; } = new(); //связанные услуги
        public int PurchaseCount { get; set; } //количество купленных
        public string Country { get; set; } = ""; //страна доставки
        public string Manufacturer { get; set; } = ""; //производство
    }
}