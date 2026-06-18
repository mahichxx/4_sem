using System;

namespace DAL004
{
    public interface IRepository : IDisposable
    {
        string BasePath { get; }
        Celebrity[] getAllCelebrities();
        Celebrity? getCelebrityById(int id);
        Celebrity[] getCelebritiesBySurname(string Surname);
        string? getPhotoPathById(int id);

        int? addCelebrity(Celebrity celebrity);            
        bool delCelebrityById(int id);                     
        int? updCelebrityById(int id, Celebrity celebrity); 
        int SaveChanges();                        
    }
    public class Celebrity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PhotoPath { get; set; }

        public Celebrity() { } 
        public Celebrity(int id, string firstname, string surname, string photoPath)
        {
            Id = id;
            Firstname = firstname;
            Surname = surname;
            PhotoPath = photoPath;
        }
    }
}