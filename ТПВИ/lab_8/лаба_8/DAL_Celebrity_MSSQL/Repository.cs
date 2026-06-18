using Microsoft.EntityFrameworkCore;
using DAL_Celebrity_MSSQL; 

namespace DAL_Celebrity_MSSQL
{
    // Исправленный интерфейс: убрали <T>, так как в Program.cs и API путаница с версиями
    public interface IRepository : IDisposable
    {
        List<Celebrity> GetAllCelebrities();
        Celebrity? GetCelebrityById(int id);
        int GetCelebrityIdByName(string name);
        bool AddCelebrity(Celebrity celebrity);
        bool UpdCelebrity(int id, Celebrity celebrity);
        bool DelCelebrity(int id);

        List<Lifeevent> GetAllLifeevents();
        Lifeevent? GetLifeeventById(int id); // Опечатка исправлена
        bool AddLifeevent(Lifeevent lifeevent);
        bool UpdLifeevent(int id, Lifeevent lifeevent);
        bool DelLifeevent(int id);

        List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId);
        Celebrity? GetCelebrityByLifeeventId(int lifeeventId);
    }

    public class Repository : IRepository
    {
        private readonly Context _context;
        private bool _disposed;

        public Repository(string connectionString)
        {
            _context = new Context(connectionString);
        }

        public static IRepository Create(string connectionString) => new Repository(connectionString);

        // --- Celebrities ---
        public List<Celebrity> GetAllCelebrities() => _context.Celebrities.AsNoTracking().ToList();
        public Celebrity? GetCelebrityById(int id) => _context.Celebrities.Find(id);

        public int GetCelebrityIdByName(string name) =>
            _context.Celebrities.AsNoTracking().FirstOrDefault(x => x.FullName.Contains(name))?.Id ?? 0;

        public bool AddCelebrity(Celebrity celebrity) { _context.Celebrities.Add(celebrity); return _context.SaveChanges() > 0; }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            var ex = _context.Celebrities.Find(id);
            if (ex == null) return false;
            _context.Entry(ex).CurrentValues.SetValues(celebrity);
            return _context.SaveChanges() > 0;
        }

        public bool DelCelebrity(int id)
        {
            var c = _context.Celebrities.Find(id);
            if (c == null) return false;
            _context.Celebrities.Remove(c);
            return _context.SaveChanges() > 0;
        }

        // --- Lifeevents ---
        public List<Lifeevent> GetAllLifeevents() => _context.Lifeevents.AsNoTracking().ToList();
        public Lifeevent? GetLifeeventById(int id) => _context.Lifeevents.Find(id);

        public bool AddLifeevent(Lifeevent lifeevent) { _context.Lifeevents.Add(lifeevent); return _context.SaveChanges() > 0; }

        public bool UpdLifeevent(int id, Lifeevent lifeevent)
        {
            var ex = _context.Lifeevents.Find(id);
            if (ex == null) return false;
            _context.Entry(ex).CurrentValues.SetValues(lifeevent);
            return _context.SaveChanges() > 0;
        }

        public bool DelLifeevent(int id)
        {
            var l = _context.Lifeevents.Find(id);
            if (l == null) return false;
            _context.Lifeevents.Remove(l);
            return _context.SaveChanges() > 0;
        }

        public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId) =>
            _context.Lifeevents.AsNoTracking().Where(l => l.CelebrityId == celebrityId).ToList();

        public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
        {
            var le = _context.Lifeevents.Find(lifeeventId);
            return le != null ? _context.Celebrities.Find(le.CelebrityId) : null;
        }

        public void Dispose() { if (!_disposed) { _context.Dispose(); _disposed = true; } }
    }
}