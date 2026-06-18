using Microsoft.EntityFrameworkCore;

namespace DAL_Celebrity_MSSQL
{
    public class Context : DbContext
    {
        private readonly string _connectionString;

        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Celebrity> Celebrities { get; set; } = null!;
        public DbSet<Lifeevent> Lifeevents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Celebrity>(entity =>
            {
                entity.ToTable("Celebrities");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Nationality).IsRequired().HasMaxLength(2);
                entity.Property(e => e.ReqPhotoPath).HasMaxLength(500);
            });

            modelBuilder.Entity<Lifeevent>(entity =>
            {
                entity.ToTable("Lifeevents");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ReqPhotoPath).HasMaxLength(500);

                entity.HasOne<Celebrity>()
                      .WithMany()
                      .HasForeignKey(e => e.CelebrityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
