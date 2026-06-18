namespace ASPA004_1
{
    // Ошибка: не нашли человека по ID
    public class FoundByIdException : Exception
    {
        public FoundByIdException(string message) : base(message) { }
    }

    // Ошибка: не удалось сохранить данные
    public class SaveException : Exception
    {
        public SaveException(string message) : base(message) { }
    }

    // Ошибка: не удалось добавить запись
    public class AddCelebrityException : Exception
    {
        public AddCelebrityException(string message) : base(message) { }
    }
}