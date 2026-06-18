internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Фильтруем системные сообщения, чтобы видеть только наши ошибки
        builder.Logging.AddFilter("Microsoft.AspNetCore.Diagnostics", LogLevel.None);

        var app = builder.Build();

        // 11. Подключаем обработчик исключений. 
        // Если где-то в коде случится "взрыв", программа перейдет по адресу /error
        app.UseExceptionHandler("/error");

        app.MapGet("/", () => "Start Page");

        // Эндпоинт 1: Пользовательское исключение
        app.MapGet("/test1", () =>
        {
            throw new Exception("-- Exception Test --"); // Специально бросаем ошибку
        });

        // Эндпоинт 2: Деление на ноль
        app.MapGet("/test2", () =>
        {
            int x = 0, y = 5;
            int z = y / x; // ОШИБКА ЗДЕСЬ
            return "test2";
        });

        // Эндпоинт 3: Выход за пределы массива
        app.MapGet("/test3", () =>
        {
            int[] x = new int[3] { 1, 2, 3 };
            int y = x[10]; // ОШИБКА ЗДЕСЬ (индекса 10 нет)
            return "test3";
        });

        // ОБРАБОТЧИК ОШИБОК (то самое место, куда попадает программа при сбое)
        app.Map("/error", async (ILogger<Program> logger, HttpContext context) =>
        {
            // Получаем информацию о том, что именно сломалось
            var exobj = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

            // Пишем ошибку в консоль
            logger.LogError(exobj?.Error, "ExceptionHandler");

            // Выводим пользователю красивое сообщение
            await context.Response.WriteAsync($"<h1>Oops! Error: {exobj?.Error.Message}</h1>");
        });

        app.Run();
    }
}