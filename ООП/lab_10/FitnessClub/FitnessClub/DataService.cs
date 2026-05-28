using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FitnessClub
{
    public class DataService
    {
        private readonly string _filePath = "data/services.json";

        public List<Service> LoadServices()
        {
            if (!File.Exists(_filePath))
                return GetDefaultServices();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Service>>(json) ?? new();
        }

        public void SaveServices(List<Service> services)
        {
            Directory.CreateDirectory("data");
            var json = JsonConvert.SerializeObject(services, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        private List<Service> GetDefaultServices()
        {
            return new List<Service>
            {
                new Service { Id=1, ShortName="Йога", FullName="Йога для начинающих",
                    Description="Расслабляющие занятия йогой для тех, кто только начинает.",
                    Category="Групповые", Rating=4.8, Price=1500, DiscountPercent=10,
                    InStock=true, PurchaseCount=120, Country="Россия",
                    Manufacturer="FitnessClub Pro", Images=new(){"assets/yoga.jpg"} },
                new Service { Id=2, ShortName="Кардио", FullName="Кардио-тренировка",
                    Description="Интенсивные кардио-нагрузки для сжигания калорий.",
                    Category="Групповые", Rating=4.6, Price=1200, DiscountPercent=0,
                    InStock=true, PurchaseCount=95, Country="Россия",
                    Manufacturer="FitnessClub Pro", Images=new(){"assets/cardio.jpg"} },
                new Service { Id=3, ShortName="Персональная", FullName="Персональная тренировка",
                    Description="Индивидуальный подход с личным тренером.",
                    Category="Индивидуальные", Rating=5.0, Price=3500, DiscountPercent=5,
                    InStock=true, PurchaseCount=60, Country="Россия",
                    Manufacturer="FitnessClub Pro", Images=new(){"assets/personal.jpg"} },
                new Service { Id=4, ShortName="Бассейн", FullName="Плавание в бассейне",
                    Description="Занятия плаванием в крытом бассейне.",
                    Category="Водные", Rating=4.9, Price=2000, DiscountPercent=15,
                    InStock=false, PurchaseCount=80, Country="Россия",
                    Manufacturer="FitnessClub Pro", Images=new(){"assets/pool.jpg"} },
            };
        }
    }
}