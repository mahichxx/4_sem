using DAL004;

namespace Validation
{
    public class SurnameFilter : IEndpointFilter
    {
        public static IRepository repository;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null)
                return Results.Problem(detail: "celebrity == null", statusCode: 500);

            if (string.IsNullOrEmpty(celebrity.Surname) || celebrity.Surname.Length < 2)
                return Results.Conflict("Value:POST /Celebrities error, Surname is wrong");

            if (repository.getAllCelebrities().Any(c => c.Surname == celebrity.Surname))
                return Results.Conflict("Value:POST /Celebrities error, Surname is doubled");

            return await next(context);
        }
    }

    public class PhotoExistFilter : IEndpointFilter
    {
        public static IRepository repository;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);
            var result = await next(context);

            if (celebrity != null)
            {
                string fileName = Path.GetFileName(celebrity.PhotoPath);
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Celebrities", fileName);

                if (!File.Exists(fullPath))
                {
                    context.HttpContext.Response.Headers.Append("X-Celebrity", $"NotFound={fileName}");
                }
            }
            return result;
        }
    }
    public class PutValidationFilter : IEndpointFilter
    {
        public static IRepository? repository;
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);
            var celebrity = context.GetArgument<Celebrity>(1);

            if (celebrity == null)
                return Results.BadRequest("Object for update is null");
            if (repository?.getCelebrityById(id) == null)
                return Results.NotFound($"Update error: Celebrity with Id={id} not found");

            if (string.IsNullOrEmpty(celebrity.Surname) || celebrity.Surname.Length < 2)
                return Results.Conflict("Update error: Surname is short or empty");

            return await next(context);
        }
    }
    public class DeleteValidationFilter : IEndpointFilter
    {
        public static IRepository? repository;
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);
            if (id <= 0)
                return Results.BadRequest("Delete error: ID must be positive");
            if (repository?.getCelebrityById(id) == null)
                return Results.NotFound($"Delete error: Celebrity with Id={id} not found");

            return await next(context);
        }
    }
}