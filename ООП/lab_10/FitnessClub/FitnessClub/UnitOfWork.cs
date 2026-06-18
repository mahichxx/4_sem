using System;

namespace FitnessClub
{
    public class UnitOfWork : IDisposable
    {
        private FitnessDbContext _db = new FitnessDbContext();
        private EFRepository<Service> _serviceRepository;
        private EFRepository<Category> _categoryRepository;

        // Репозиторий услуг
        public EFRepository<Service> Services =>
            _serviceRepository ?? (_serviceRepository = new EFRepository<Service>(_db));

        // Репозиторий категорий
        public EFRepository<Category> Categories =>
            _categoryRepository ?? (_categoryRepository = new EFRepository<Category>(_db));

        // Тот самый метод, который сохраняет ВСЁ сразу
        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) _db.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}