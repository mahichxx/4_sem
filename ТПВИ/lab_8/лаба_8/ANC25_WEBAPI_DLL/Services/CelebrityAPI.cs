using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL_Celebrity_MSSQL;

namespace ANC25_WEBAPI_DLL.Services
{
    public static class CelebrityAPI
    {
        public static IEndpointRouteBuilder MapCelebrities(this IEndpointRouteBuilder routeBuilder, string prefix = "/api/Celebrities")
        {
            var celebrities = routeBuilder.MapGroup(prefix);
            // Получить всех знаменитостей
            celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());

            // Получить одну знаменитость по ID
            celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var celebrity = repo.GetCelebrityById(id);
                return celebrity != null ? Results.Ok(celebrity) : Results.NotFound($"Знаменитость с ID {id} не найдена");
            });

            // Удалить знаменитость
            celebrities.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                if (repo.DelCelebrity(id))
                    return Results.Ok(new { message = $"Знаменитость {id} удалена" });

                return Results.NotFound(new { error = "Not Found", detail = $"Celebrity Id = {id}" });
            });

            // Добавить новую знаменитость
            celebrities.MapPost("/", (IRepository repo, Celebrity celebrity) =>
            {
                if (celebrity == null) return Results.Problem("Данные не получены");
                repo.AddCelebrity(celebrity);
                return Results.Created($"/api/Celebrities/{celebrity.Id}", celebrity);
            });

            // Обновить данные знаменитости
            // Исправлено: передаем (id, celebrity), так как в Repository.cs метод принимает 2 аргумента
            celebrities.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Celebrity celebrity) =>
            {
                if (celebrity == null) return Results.Problem("Данные не получены");
                celebrity.Id = id;

                if (repo.UpdCelebrity(id, celebrity))
                    return Results.Ok(celebrity);

                return Results.NotFound();
            });

            // Эндпоинт для получения фото
            celebrities.MapGet("/photo/{fname}", async (IOptions<CelebritiesConfig> iconfig, string fname) =>
            {
                string path = Path.Combine(iconfig.Value.PhotoPath, fname);
                if (!File.Exists(path)) return Results.NotFound();

                var bytes = await File.ReadAllBytesAsync(path);
                return Results.File(bytes, "image/jpeg");
            });

            return celebrities;
        }

        public static IEndpointRouteBuilder MapLifeevents(this IEndpointRouteBuilder routeBuilder, string prefix = "/api/Lifeevents")
        {
            var lifeevents = routeBuilder.MapGroup(prefix);

            // Все события
            lifeevents.MapGet("/", (IRepository repo) => repo.GetAllLifeevents());

            // Исправлена опечатка: GetLifeeventById (было GetLifeevetById)
            lifeevents.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var le = repo.GetLifeeventById(id);
                return le != null ? Results.Ok(le) : Results.NotFound($"Событие {id} не найдено");
            });

            // События конкретной знаменитости
            lifeevents.MapGet("/Celebrities/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var list = repo.GetLifeeventsByCelebrityId(id);
                return list.Any() ? Results.Ok(list) : Results.NotFound();
            });

            // Удаление события
            lifeevents.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
                repo.DelLifeevent(id) ? Results.Ok() : Results.NotFound());

            // Добавление события
            lifeevents.MapPost("/", (IRepository repo, Lifeevent lifeevent) =>
            {
                if (lifeevent == null) return Results.Problem();
                repo.AddLifeevent(lifeevent);
                return Results.Ok(lifeevent);
            });

            // Обновление события
            // Исправлено: передаем (id, lifeevent)
            lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Lifeevent lifeevent) =>
            {
                if (lifeevent == null) return Results.NotFound();
                lifeevent.Id = id;
                if (repo.UpdLifeevent(id, lifeevent))
                    return Results.Ok(lifeevent);

                return Results.NotFound();
            });

            return lifeevents;
        }

        public static RouteHandlerBuilder MapPhotoCelebrities(this IEndpointRouteBuilder routebuilder, string? prefix = "/Photos")
        {
            return routebuilder.MapGet($"{prefix}/{{fname}}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
            {
                string filePath = Path.Combine(iconfig.Value.PhotoPath, fname);
                if (!File.Exists(filePath)) return;

                using var fileStream = File.OpenRead(filePath);
                context.Response.ContentType = "image/jpeg";
                await fileStream.CopyToAsync(context.Response.Body);
            });
        }
    }
}