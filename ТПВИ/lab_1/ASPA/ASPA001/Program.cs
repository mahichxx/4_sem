using Microsoft.AspNetCore.HttpLogging; // Подключаем библиотеку для ведения логов (журнала) посещений

internal class Program // Объявляем основной класс программы
{
    private static void Main(string[] args) // Главный метод — точка входа, откуда начинается выполнение программы
    {
        // Создаем объект-строитель (builder), который собирает настройки нашего будущего веб-сервера
        var builder = WebApplication.CreateBuilder(args);

        // В секцию сервисов добавляем HTTPLogging — службу, которая будет следить за запросами к серверу
        builder.Services.AddHttpLogging(logging =>
        {
            // Настраиваем логирование так, чтобы в консоли отображались все возможные данные (заголовки, тело и т.д.)
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        });

        // Создаем само приложение (объект app) на основе всех настроек, сделанных в builder
        var app = builder.Build();

        // Подключаем промежуточное ПО (Middleware) для логирования — теперь каждый запрос будет записываться в консоль
        app.UseHttpLogging();

        // Описываем маршрут: если кто-то обратится по адресу "/", вернуть текстовое сообщение
        app.MapGet("/", () => "Мое первое ASPA");

        // Запускаем веб-сервер и начинаем «слушать» входящие запросы от браузеров
        app.Run();
    }
}