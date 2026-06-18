using DAL003; 
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDirectoryBrowser();

var app = builder.Build();

//Чтобы не копировать папки
string masterPath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "DAL003", "Celebrities"));

Repository.JSONFileName = "Celebrities.json";

// Мы создаем один репозиторий, который будет жить, пока работает сайт
using (IRepository repository = Repository.Create(masterPath))
{
    app.MapGet("/Celebrities", () => repository.getAllCelebrities());

    app.MapGet("/Celebrities/{id:int}", (int id) => repository.getCelebrityById(id));

    app.MapGet("/Celebrities/BySurname/{surname}", (string surname) =>
        repository.getCelebritiesBySurname(surname));

    app.MapGet("/Celebrities/PhotoPathById/{id:int}", (int id) =>
        repository.getPhotoPathById(id));

    app.MapGet("/", () => "Hello World! Сервер работает. Используйте /Celebrities для просмотра данных.");

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(masterPath),
        RequestPath = "/Celebrities/download", 
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Content-Disposition",
                "attachment; filename=\"" + ctx.File.Name + "\"");
        }
    });
   
    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(masterPath),
        RequestPath = "/Celebrities/download"
    });

    app.Run();
}