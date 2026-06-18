using DAL_Celebrity;
using Microsoft.EntityFrameworkCore;

namespace DAL_Celebrity_MSSQL
{
    public class Repository : IRepository<Celebrity, Lifeevent>
    {
        private readonly Context _context;
        private bool _disposed;

        private Repository(string connectionString)
        {
            _context = new Context(connectionString);
        }

        public static IRepository<Celebrity, Lifeevent> Create(string connectionString)
            => new Repository(connectionString);

        public List<Celebrity> GetAllCelebrities()
            => _context.Celebrities.AsNoTracking().ToList();

        public Celebrity? GetCelebrityById(int Id)
            => _context.Celebrities.Find(Id);

        public bool DelCelebrity(int id)
        {
            var c = _context.Celebrities.Find(id);
            if (c == null) return false;
            _context.Celebrities.Remove(c);
            _context.SaveChanges();
            return true;
        }

        public bool AddCelebrity(Celebrity celebrity)
        {
            _context.Celebrities.Add(celebrity);
            _context.SaveChanges();
            return true;
        }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            var existing = _context.Celebrities.Find(id);
            if (existing == null) return false;
            existing.Update(celebrity);
            _context.SaveChanges();
            return true;
        }

        public int GetCelebrityIdByName(string name)
        {
            var c = _context.Celebrities
                .AsNoTracking()
                .FirstOrDefault(x => x.FullName.Contains(name));
            return c?.Id ?? 0;
        }

        public List<Lifeevent> GetAllLifeevents()
            => _context.Lifeevents.AsNoTracking().ToList();

        public Lifeevent? GetLifeevetById(int Id)
            => _context.Lifeevents.Find(Id);

        public bool DelLifeevent(int id)
        {
            var l = _context.Lifeevents.Find(id);
            if (l == null) return false;
            _context.Lifeevents.Remove(l);
            _context.SaveChanges();
            return true;
        }

        public bool AddLifeevent(Lifeevent lifeevent)
        {
            _context.Lifeevents.Add(lifeevent);
            _context.SaveChanges();
            return true;
        }

        public bool UpdLifeevent(int id, Lifeevent lifeevent)
        {
            var existing = _context.Lifeevents.Find(id);
            if (existing == null) return false;
            existing.Update(lifeevent);
            _context.SaveChanges();
            return true;
        }

        public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId)
            => _context.Lifeevents
                .AsNoTracking()
                .Where(l => l.CelebrityId == celebrityId)
                .ToList();

        public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
        {
            var life = _context.Lifeevents.AsNoTracking().FirstOrDefault(l => l.Id == lifeeventId);
            if (life == null) return null;
            return _context.Celebrities.Find(life.CelebrityId);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _context.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
