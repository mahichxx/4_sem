using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL004
{
    public interface IRepository : IDisposable
    {
        string BasePath { get; }

        // Методы из прошлой лабы (Чтение)
        Celebrity[] getAllCelebrities();
        Celebrity? getCelebrityById(int id);
        Celebrity[] getCelebritiesBySurname(string surname);
        string? getPhotoPathById(int id);

        // НОВЫЕ методы (Изменение данных)
        int? addCelebrity(Celebrity celebrity);               // Добавить
        bool delCelebrityById(int id);                       // Удалить
        int? updCelebrityById(int id, Celebrity celebrity);    // Изменить
        int SaveChanges();                                   // Сохранить в JSON-файл
    }
}