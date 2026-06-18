using DAL003; // Подключаем твою библиотеку

namespace Test_DAL003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Указываем имя файла
            Repository.JSONFileName = "Celebrities.json";

            // Конструкция using сама закроет файл в конце
            using (IRepository repository = Repository.Create("Celebrities"))
            {
                // Выводим всех
                foreach (Celebrity celebrity in repository.getAllCelebrities())
                {
                    Console.WriteLine($"Id = {celebrity.Id}, FirstName = {celebrity.FirstName}, " +
                                      $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath}");
                }

                Console.WriteLine("\n--- Тест поиска по Id=1 ---");
                Celebrity? celebrity1 = repository.getCelebrityById(1);
                if (celebrity1 != null)
                {
                    Console.WriteLine($"Нашел: {celebrity1.FirstName} {celebrity1.Surname}");
                }

                Console.WriteLine("\n--- Тест поиска по фамилии 'Chomsky' ---");
                foreach (var c in repository.getCelebritiesBySurname("Chomsky"))
                {
                    Console.WriteLine($"Нашел: {c.FirstName} {c.Surname}");
                }
            }

            Console.WriteLine("\nТест окончен. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}