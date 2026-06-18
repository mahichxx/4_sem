using DAL004;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";
var repository = Repository.Create("Celebrities");

app.UseExceptionHandler("/Celebrities/Error");

app.MapGet("/Celebrities", () => repository.getAllCelebrities());

app.MapGet("/Celebrities/{id:int}", (int id) => {
    var celebrity = repository.getCelebrityById(id);
    if (celebrity == null) throw new FoundByIdException($"Celebrity Id = {id}");
    return Results.Ok(celebrity);
});

app.MapPost("/Celebrities", (Celebrity celebrity) => {
    int? id = repository.addCelebrity(celebrity);
    if (id == null) throw new AddCelebrityException("/Celebrities error, id == null");
    if (repository.SaveChanges() <= 0) throw new SaveException("/Celebrities error, SaveChanges <= 0");
    return Results.Ok(repository.getCelebrityById((int)id));
})
.AddEndpointFilter(async (context, next) => {
    var celebrity = context.GetArgument<Celebrity>(0);
    if (celebrity == null)
        return Results.Problem(detail: "celebrity == null", statusCode: 500);

    if (string.IsNullOrEmpty(celebrity.Surname) || celebrity.Surname.Length < 2)
        return Results.Conflict("Value:POST /Celebrities error, Surname is wrong");

    return await next(context);
})
.AddEndpointFilter(async (context, next) => {
    var celebrity = context.GetArgument<Celebrity>(0);
    if (celebrity == null)
        return Results.Problem(detail: "celebrity == null", statusCode: 500);

    if (repository.getAllCelebrities().Any(c => c.Surname == celebrity.Surname))
        return Results.Conflict("Value:POST /Celebrities error, Surname is doubled");

    return await next(context);
})
.AddEndpointFilter(async (context, next) => {
    var celebrity = context.GetArgument<Celebrity>(0);

    if (celebrity == null)
        return Results.Problem(detail: "POST /Celebrities error, celebrity == null", statusCode: 500);

    string fileName = Path.GetFileName(celebrity.PhotoPath);
    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Celebrities", fileName);
    var result = await next(context);
    if (!File.Exists(fullPath))
    {
        context.HttpContext.Response.Headers.Add("X-Celebrity", $"NotFound={fileName}");
    }

    return result;
});
app.MapDelete("/Celebrities/{id:int}", (int id) => {
    if (!repository.delCelebrityById(id))
        throw new DeleteByIdException($"DELETE /Celebrities error, Id = {id}");

    repository.SaveChanges();

    return Results.Ok($"Celebrity with Id = {id} deleted");
});
app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) => {
    int? newid = null;
    if ((newid = repository.updCelebrityById(id, celebrity)) == null)
        throw new UpdException($"Id={id}");
    repository.SaveChanges();
    return new Celebrity((int)newid, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
});

app.Map("/Celebrities/Error", (HttpContext ctx) => {
    var feature = ctx.Features.Get<IExceptionHandlerFeature>();
    var ex = feature?.Error;
    IResult rc = Results.Problem(
       detail: "Panic",
       instance: app.Environment.EnvironmentName,
       title: "ASPA004",
       statusCode: 500
   );

    if (ex != null)
    {
        if (ex is FoundByIdException)
        {
            rc = Results.Problem(detail: ex.Message, statusCode: 404);
        }
        if (ex is DeleteByIdException) rc = Results.NotFound(ex.Message);
        if (ex is UpdException) rc = Results.NotFound(ex.Message);
    }

    return rc;
});

app.MapFallback((HttpContext ctx) =>
{
    return Results.NotFound(new
    {
        error = $"path {ctx.Request.Path} not supported"
    });
});
app.Run();

public class FoundByIdException : Exception
{
    public FoundByIdException(string message) : base($"Found by Id: {message}") { }
}
public class SaveException : Exception
{
    public SaveException(string message) : base($"SaveChanges error: {message}") { }
}
public class AddCelebrityException : Exception
{
    public AddCelebrityException(string message) : base($"AddCelebrityException error: {message}") { }
}
public class DeleteByIdException : Exception { public DeleteByIdException(string message) : base($"Delete by Id:{message}") { } };
public class UpdException : Exception
{
    public UpdException(string message) : base($"Update by Id:{message}") { }
};