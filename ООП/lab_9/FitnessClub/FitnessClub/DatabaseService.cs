using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub
{
    public static class DatabaseService
    {
        public static string ConnString => System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

        // Возвращаем имя для совместимости с MainWindow.xaml.cs
        public static void InitializeDatabase()
        {
            using (var db = new FitnessDbContext())
            {
                // Просто проверяем связь с базой. EF сам создаст таблицы, если их нет.
                db.Database.Initialize(force: false);
            }
        }

        public static async Task<List<Service>> GetAllServicesAsync()
        {
            using (var db = new FitnessDbContext())
            {
                return await db.Services.ToListAsync(); //просмотр
            }
        }

        // Возвращаем имя для совместимости с MainViewModel
        public static void SaveServiceWithTransaction(Service service, bool isNew)
        {
            using (var db = new FitnessDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (isNew) db.Services.Add(service); //добавить
                        else
                        {
                            db.Services.Attach(service);
                            db.Entry(service).State = EntityState.Modified; //просмотр
                        }
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch { transaction.Rollback(); throw; }
                }
            }
        }

        // Возвращаем имя для совместимости с MainViewModel
        public static void DeleteService(int id)
        {
            using (var db = new FitnessDbContext())
            {
                var service = db.Services.Find(id);
                if (service != null)
                {
                    db.Services.Remove(service); //удаление
                    db.SaveChanges();
                }
            }
        }
    }
}