using System.Data.Entity;

namespace FitnessClub
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext() : base("name=DefaultConnection")
        {
            // ОТКЛЮЧАЕМ ВСЕ ПРОВЕРКИ И ОБНОВЛЕНИЯ:
            Database.SetInitializer<FitnessDbContext>(null);
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}