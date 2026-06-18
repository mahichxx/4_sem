using DAL004;
using ASPA004_1; 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string masterPath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "DAL004", "Celebrities"));
Repository.JSONFileName = "Celebrities.json";

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature?.Error;

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = "An error occurred",
            Instance = context.Request.Path,
            //Добавляем подробности об ошибке в поле Detail
            Detail = $"Message: {exception?.Message}. Path: {Path.Combine(masterPath, Repository.JSONFileName)}",
            Status = exception switch
            {
                FoundByIdException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            }
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

// 1. GET ALL
app.MapGet("/Celebrities", () => {
    using var repo = Repository.Create(masterPath);
    return Results.Ok(repo.getAllCelebrities());
});

// 2. GET BY ID
app.MapGet("/Celebrities/{id:int}", (int id) => {
    using var repo = Repository.Create(masterPath);
    var celebrity = repo.getCelebrityById(id);
    if (celebrity == null) throw new FoundByIdException($"Celebrity with Id = {id} not found");
    return Results.Ok(celebrity);
});

// 3. POST (Добавление)
app.MapPost("/Celebrities", (Celebrity celebrity) => {
    using var repo = Repository.Create(masterPath);
    var newId = repo.addCelebrity(celebrity);
    repo.SaveChanges();
    return Results.Created($"/Celebrities/{newId}", celebrity with { Id = (int)newId });
});

// 4. PUT (Обновление - Задание 31)
app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) => {
    using var repo = Repository.Create(masterPath);
    var updatedId = repo.updCelebrityById(id, celebrity);
    if (updatedId == null) throw new FoundByIdException($"Update error: Id = {id} not found");
    repo.SaveChanges();
    return Results.Ok(celebrity with { Id = id });
});

// 5. DELETE (Удаление - Задание 25)
app.MapDelete("/Celebrities/{id:int}", (int id) => {
    using var repo = Repository.Create(masterPath);
    bool deleted = repo.delCelebrityById(id);
    if (!deleted) throw new FoundByIdException($"Delete error: Id = {id} not found");
    repo.SaveChanges();
    return Results.Ok($"Celebrity with Id = {id} deleted");
});

// Заглушка для неизвестных адресов
app.MapFallback(() => Results.NotFound(new { error = "Route not supported" }));

app.Run();