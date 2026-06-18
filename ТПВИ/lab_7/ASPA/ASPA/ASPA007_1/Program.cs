using ASPA007_1;
using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Подключаем конфиг
builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);

// Привязка конфигурации
builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));

// Подключаем Razor Pages
builder.Services.AddRazorPages();

// Регистрируем DAL
builder.Services.AddScoped<IRepository<Celebrity, Lifeevent>>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
    return Repository.Create(cfg.ConnectionString);
});

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
