using DAL004;

namespace Test_DAL004
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository.JSONFileName = "Celebrities.json";

            // ВАЖНО: Укажи путь к папке Celebrities. 
            // Если она лежит в DAL004, используй: "../../../Celebrities"
            using (IRepository repository = Repository.Create("../../../../DAL004/Celebrities"))
            {
                // Вспомогательный метод для вывода списка в консоль
                void print(string label)
                {
                    Console.WriteLine($"--- {label} -----------------");
                    foreach (Celebrity celebrity in repository.getAllCelebrities())
                    {
                        Console.WriteLine($"Id = {celebrity.Id}, FirstName = {celebrity.FirstName}, " +
                                          $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath}");
                    }
                }

                print("start");

                // 1. ТЕСТ ДОБАВЛЕНИЯ (ADD)
                int? testdel1 = repository.addCelebrity(new Celebrity(0, "TestDel1", "TestDel1", "/Photo/TestDel1.jpg"));
                int? testdel2 = repository.addCelebrity(new Celebrity(0, "TestDel2", "TestDel2", "/Photo/TestDel2.jpg"));
                int? testupd1 = repository.addCelebrity(new Celebrity(0, "TestUpd1", "TestUpd1", "/Photo/TestUpd1.jpg"));
                int? testupd2 = repository.addCelebrity(new Celebrity(0, "TestUpd2", "TestUpd2", "/Photo/TestUpd2.jpg"));

                print("add 4");

                // 2. ТЕСТ УДАЛЕНИЯ (DELETE)
                if (testdel1 != null)
                {
                    if (repository.delCelebrityById((int)testdel1)) Console.WriteLine($"$ delete {testdel1} ");
                    else Console.WriteLine($"$ delete {testdel1} error");
                }
                if (testdel2 != null)
                {
                    if (repository.delCelebrityById((int)testdel2)) Console.WriteLine($"$ delete {testdel2} ");
                    else Console.WriteLine($"$ delete {testdel2} error");
                }

                // Проверка удаления несуществующего (Id = 1000)
                if (repository.delCelebrityById(1000)) Console.WriteLine("$ delete {1000} ");
                else Console.WriteLine("$ delete {1000} error");

                repository.SaveChanges(); // Сохраняем промежуточный результат
                print("del 2");

                // 3. ТЕСТ ОБНОВЛЕНИЯ (UPDATE)
                if (testupd1 != null)
                {
                    if (repository.updCelebrityById((int)testupd1, new Celebrity(0, "Updated1", "Updated1", "/Photo/Updated1.jpg")) != null)
                        Console.WriteLine($"$ update {testupd1} ");
                    else Console.WriteLine($"$ update {testupd1} error");
                }
                if (testupd2 != null)
                {
                    if (repository.updCelebrityById((int)testupd2, new Celebrity(0, "Updated2", "Updated2", "/Photo/Updated2.jpg")) != null)
                        Console.WriteLine($"$ update {testupd2} ");
                    else Console.WriteLine($"$ update {testupd2} error");
                }

                // Проверка обновления несуществующего
                if (repository.updCelebrityById(1000, new Celebrity(0, "Updated1000", "Updated1000", "/Photo/Updated1000.jpg")) != null)
                    Console.WriteLine("$ update {1000} ");
                else Console.WriteLine("$ update {1000} error");

                repository.SaveChanges(); // Финальное сохранение
                print("upd 2");
            }

            Console.WriteLine("\nТест завершен. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}