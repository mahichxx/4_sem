// ASPA006_1/Program.cs
using ASPA006_1;
using ASPA006_1.Middleware;
using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Подключаем конфиг
builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);

// Привязка к классу конфигурации
builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));

// Регистрируем Scoped-сервис репозитория
builder.Services.AddScoped<IRepository<Celebrity, Lifeevent>>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
    return Repository.Create(cfg.ConnectionString);
});

// Статические файлы
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Middleware ошибок
app.UseErrorHandling();

// Статические файлы и Index.html по умолчанию
app.UseDefaultFiles();
app.UseStaticFiles();

// API

app.MapGet("/api/celebrities", (IRepository<Celebrity, Lifeevent> repo) =>
{
    return Results.Ok(repo.GetAllCelebrities());
});

app.MapGet("/api/celebrities/{id:int}", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    var c = repo.GetCelebrityById(id);
    return c is null ? Results.NotFound() : Results.Ok(c);
});

app.MapPost("/api/celebrities", (Celebrity celebrity, IRepository<Celebrity, Lifeevent> repo) =>
{
    repo.AddCelebrity(celebrity);
    return Results.Created($"/api/celebrities/{celebrity.Id}", celebrity);
});

app.MapPut("/api/celebrities/{id:int}", (int id, Celebrity celebrity, IRepository<Celebrity, Lifeevent> repo) =>
{
    if (!repo.UpdCelebrity(id, celebrity)) return Results.NotFound();
    return Results.NoContent();
});

app.MapDelete("/api/celebrities/{id:int}", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    return repo.DelCelebrity(id) ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/api/celebrities/{id:int}/lifeevents", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    var events = repo.GetLifeeventsByCelebrityId(id);
    return Results.Ok(events);
});

app.MapGet("/api/lifeevents", (IRepository<Celebrity, Lifeevent> repo) =>
{
    return Results.Ok(repo.GetAllLifeevents());
});

app.MapGet("/api/lifeevents/{id:int}", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    var l = repo.GetLifeevetById(id);
    return l is null ? Results.NotFound() : Results.Ok(l);
});

app.MapPost("/api/lifeevents", (Lifeevent lifeevent, IRepository<Celebrity, Lifeevent> repo) =>
{
    repo.AddLifeevent(lifeevent);
    return Results.Created($"/api/lifeevents/{lifeevent.Id}", lifeevent);
});

app.MapPut("/api/lifeevents/{id:int}", (int id, Lifeevent lifeevent, IRepository<Celebrity, Lifeevent> repo) =>
{
    if (!repo.UpdLifeevent(id, lifeevent)) return Results.NotFound();
    return Results.NoContent();
});

app.MapDelete("/api/lifeevents/{id:int}", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    return repo.DelLifeevent(id) ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/api/lifeevents/{id:int}/celebrity", (int id, IRepository<Celebrity, Lifeevent> repo) =>
{
    var c = repo.GetCelebrityByLifeeventId(id);
    return c is null ? Results.NotFound() : Results.Ok(c);
});

app.Run();
