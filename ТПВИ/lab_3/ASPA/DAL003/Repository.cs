using System.Text.Json;

namespace DAL003;

public class Repository : IRepository
{
    public static string JSONFileName { get; set; } = "Celebrities.json";
    public string BasePath { get; }
    private Celebrity[] _celebrities;

    public Repository(string folderPath)
    {

        BasePath = Path.GetFullPath(folderPath);
        string fullPathJson = Path.Combine(BasePath, JSONFileName);

        if (!File.Exists(fullPathJson))
            throw new FileNotFoundException($"Файл не найден: {fullPathJson}");

        string jsonContent = File.ReadAllText(fullPathJson);
        _celebrities = JsonSerializer.Deserialize<Celebrity[]>(jsonContent) ?? Array.Empty<Celebrity>();
    }

    public static IRepository Create(string folderName) => new Repository(folderName);

    public Celebrity[] getAllCelebrities() => _celebrities;

    public Celebrity? getCelebrityById(int id) => _celebrities.FirstOrDefault(c => c.Id == id);

    public Celebrity[] getCelebritiesBySurname(string Surname) =>
        _celebrities.Where(c => c.Surname.Equals(Surname, StringComparison.OrdinalIgnoreCase)).ToArray();

    public string? getPhotoPathById(int id) => getCelebrityById(id)?.PhotoPath;

    public void Dispose() { _celebrities = null!; }
}