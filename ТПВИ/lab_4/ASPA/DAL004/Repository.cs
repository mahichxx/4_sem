using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DAL004;

public class Repository : IRepository
{
    public static string JSONFileName { get; set; } = "Celebrities.json";
    public string BasePath { get; }
    private List<Celebrity> _celebrities; // Теперь это Список (List), а не массив

    public Repository(string folderPath)
    {
        BasePath = Path.GetFullPath(folderPath);
        string fullPathJson = Path.Combine(BasePath, JSONFileName);
        string jsonContent = File.ReadAllText(fullPathJson);

        // Читаем из файла и превращаем в List
        _celebrities = JsonSerializer.Deserialize<List<Celebrity>>(jsonContent) ?? new List<Celebrity>();
    }

    public static IRepository Create(string folderPath) => new Repository(folderPath);

    // --- СТАРЫЕ МЕТОДЫ ---
    public Celebrity[] getAllCelebrities() => _celebrities.ToArray();
    public Celebrity? getCelebrityById(int id) => _celebrities.Find(c => c.Id == id);
    public Celebrity[] getCelebritiesBySurname(string surname) =>
        _celebrities.Where(c => c.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase)).ToArray();
    public string? getPhotoPathById(int id) => getCelebrityById(id)?.PhotoPath;

    // --- НОВЫЕ МЕТОДЫ (CRUD) ---

    public int? addCelebrity(Celebrity celebrity)
    {
        // Генерируем новый ID (берем максимальный и +1)
        int newId = _celebrities.Any() ? _celebrities.Max(c => c.Id) + 1 : 1;
        var newCeleb = celebrity with { Id = newId }; // Создаем копию объекта с новым ID
        _celebrities.Add(newCeleb);
        return newId;
    }

    public bool delCelebrityById(int id)
    {
        var target = getCelebrityById(id);
        if (target == null) return false;
        return _celebrities.Remove(target);
    }

    public int? updCelebrityById(int id, Celebrity celebrity)
    {
        var index = _celebrities.FindIndex(c => c.Id == id);
        if (index == -1) return null;

        // Заменяем старый объект новым, сохраняя ID
        _celebrities[index] = celebrity with { Id = id };
        return id;
    }

    public int SaveChanges()
    {
        // Сериализация: превращаем список обратно в текст JSON и записываем в файл
        string json = JsonSerializer.Serialize(_celebrities, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Path.Combine(BasePath, JSONFileName), json);
        return _celebrities.Count;
    }

    public void Dispose() { }
}
