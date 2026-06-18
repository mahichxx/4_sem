using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Мы не можем просто вызвать UseDefaultFiles(), так как она ищет index.html.
// Нам нужно создать "объект настроек" (options).

DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear(); // Забываем про index.html
options.DefaultFileNames.Add("Neumann.html"); // Теперь главной будет Neumann.html

app.UseDefaultFiles(options); // Применяем наши настройки

// --- 2. Разрешаем отдавать Neumann.html из wwwroot ---
app.UseStaticFiles();

// --- 3. Подключаем папку Picture по адресу /static (Пункт 9.2-3) ---
app.UseStaticFiles(new StaticFileOptions
{
    // Указываем физический путь к папке Picture на твоем компьютере
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Picture")),

    // Указываем виртуальный путь (как это будет выглядеть в браузере)
    RequestPath = "/static"
});

app.Run();