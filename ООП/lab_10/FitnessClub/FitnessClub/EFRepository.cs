using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FitnessClub
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private FitnessDbContext _db;
        private DbSet<T> _dbSet;

        public EFRepository(FitnessDbContext context)
        {
            _db = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public T Get(int id) => _dbSet.Find(id);
        public void Create(T item) => _dbSet.Add(item);
        public void Update(T item) => _db.Entry(item).State = EntityState.Modified;
        public void Delete(int id)
        {
            T item = _dbSet.Find(id);
            if (item != null) _dbSet.Remove(item);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).ToList();
    }
}