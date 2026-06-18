using DAL004;
using Validation;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";
var repo = Repository.Create("Celebrities");
SurnameFilter.repository = repo;
PhotoExistFilter.repository = repo;
PutValidationFilter.repository = repo;
DeleteValidationFilter.repository = repo;

app.UseExceptionHandler("/Celebrities/Error");

var api = app.MapGroup("/Celebrities");

api.MapGet("/", () => repo.getAllCelebrities());

api.MapGet("/{id:int}", (int id) => {
    var c = repo.getCelebrityById(id);
    if (c == null) throw new FoundByIdException($"Id = {id}");
    return Results.Ok(c);
});

api.MapPost("/", (Celebrity celebrity) => {
    int? id = repo.addCelebrity(celebrity);
    if (id == null) throw new AddCelebrityException("id == null");
    repo.SaveChanges();
    return Results.Ok(repo.getCelebrityById((int)id));
})
.AddEndpointFilter<SurnameFilter>()
.AddEndpointFilter<PhotoExistFilter>();
api.MapDelete("/{id:int}", (int id) => {
    repo.delCelebrityById(id);
    repo.SaveChanges();
    return Results.Ok($"Celebrity with Id = {id} deleted");
})
.AddEndpointFilter<DeleteValidationFilter>();
api.MapPut("/{id:int}", (int id, Celebrity celebrity) => {
    repo.updCelebrityById(id, celebrity);
    repo.SaveChanges();
    return Results.Ok(celebrity);
})
.AddEndpointFilter<PutValidationFilter>();


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