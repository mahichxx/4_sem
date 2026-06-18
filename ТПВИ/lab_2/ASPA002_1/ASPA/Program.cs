var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ПЕРВЫМ: включаем поиск индексных файлов (Index.html)
app.UseDefaultFiles();

// ВТОРЫМ: разрешаем серверу отдавать статические файлы из wwwroot
app.UseStaticFiles();

// ТРЕТЬИМ (для задания): приветственная страница
app.UseWelcomePage("/aspnetcore");

app.Run();