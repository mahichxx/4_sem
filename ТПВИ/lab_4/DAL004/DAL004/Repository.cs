using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DAL004
{
    public class Repository : IRepository
    {
        public static string JSONFileName { get; set; } = "Celebrities.json";
        private List<Celebrity> _celebrities; 
        public string BasePath { get; }
        private string FullPath => Path.Combine(BasePath, JSONFileName);

        public Repository(string directoryName)
        {
            BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryName);

            if (File.Exists(FullPath))
            {
                string jsonContent = File.ReadAllText(FullPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _celebrities = JsonSerializer.Deserialize<List<Celebrity>>(jsonContent, options) ?? new List<Celebrity>();
            }
            else
            {
                _celebrities = new List<Celebrity>();
            }
        }

        public Celebrity[] getAllCelebrities() => _celebrities.ToArray();

        public Celebrity? getCelebrityById(int id) => _celebrities.FirstOrDefault(c => c.Id == id);

        public Celebrity[] getCelebritiesBySurname(string Surname) =>
            _celebrities.Where(c => c.Surname.Equals(Surname, StringComparison.OrdinalIgnoreCase)).ToArray();

        public string? getPhotoPathById(int id) => getCelebrityById(id)?.PhotoPath;

        public int? addCelebrity(Celebrity celebrity)
        {
            int newId = _celebrities.Count > 0 ? _celebrities.Max(c => c.Id) + 1 : 1;
            celebrity.Id = newId;
            _celebrities.Add(celebrity);
            return newId;
        }

        public bool delCelebrityById(int id)
        {
            var item = getCelebrityById(id);
            if (item != null)
            {
                _celebrities.Remove(item);
                return true;
            }
            return false;
        }

        public int? updCelebrityById(int id, Celebrity celebrity)
        {
            var index = _celebrities.FindIndex(c => c.Id == id);
            if (index != -1)
            {
                celebrity.Id = id;
                _celebrities[index] = celebrity;
                return id;
            }
            return null;
        }

        public int SaveChanges()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_celebrities, options);
                File.WriteAllText(FullPath, jsonString);
                return 1; 
            }
            catch { return 0; }
        }

        public static Repository Create(string directoryName) => new Repository(directoryName);

        public void Dispose()
        {
            _celebrities.Clear();
            GC.SuppressFinalize(this);
        }
    }
}