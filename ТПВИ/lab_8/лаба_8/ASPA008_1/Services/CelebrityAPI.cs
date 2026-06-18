using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO; // Добавлено для Path и File
using System;
using System.Collections.Generic;
using System.Linq;
using DAL_Celebrity_MSSQL;

namespace ASPA008_1.Services
{
    public static class CelebrityAPI
    {
        public static IEndpointRouteBuilder MapCelebrities(this IEndpointRouteBuilder routeBuilder, string prefix = "/api/Celebrities")
        {
            var celebrities = routeBuilder.MapGroup(prefix);

            // Получить всех
            celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());

            // Получить одного по ID
            celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var celebrity = repo.GetCelebrityById(id);
                return celebrity != null ? Results.Ok(celebrity) : Results.NotFound();
            });

            // Удалить
            celebrities.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
                repo.DelCelebrity(id) ? Results.Ok() : Results.NotFound());

            // Добавить
            celebrities.MapPost("/", (IRepository repo, Celebrity celebrity) =>
            {
                if (celebrity == null) return Results.Problem();
                repo.AddCelebrity(celebrity);
                return Results.Ok(celebrity);
            });

            // Обновить (ИСПРАВЛЕНО: передаем и id, и объект)
            celebrities.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Celebrity celebrity) =>
            {
                if (celebrity == null) return Results.Problem();
                celebrity.Id = id;

                if (repo.UpdCelebrity(id, celebrity))
                    return Results.Ok(celebrity);
                else
                    return Results.NotFound();
            });

            // Получить фото
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

            // Событие по ID
            lifeevents.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var le = repo.GetLifeeventById(id);
                return le != null ? Results.Ok(le) : Results.NotFound();
            });

            // Удалить событие
            lifeevents.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
                repo.DelLifeevent(id) ? Results.Ok() : Results.NotFound());

            // Добавить событие
            lifeevents.MapPost("/", (IRepository repo, Lifeevent lifeevent) =>
            {
                if (lifeevent == null) return Results.Problem();
                repo.AddLifeevent(lifeevent);
                return Results.Ok(lifeevent);
            });

            // Обновить событие (ИСПРАВЛЕНО: убран лишний return перед вызовом и добавлено два аргумента)
            lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Lifeevent lifeevent) =>
            {
                if (lifeevent == null) return Results.NotFound();
                lifeevent.Id = id;

                if (repo.UpdLifeevent(id, lifeevent))
                    return Results.Ok(lifeevent);
                else
                    return Results.NotFound();
            });

            return lifeevents;
        }

        public static RouteHandlerBuilder MapPhotoCelebrities(this IEndpointRouteBuilder routebuilder, string? prefix = "/Photos")
        {
            if (string.IsNullOrEmpty(prefix))
                prefix = routebuilder.ServiceProvider.GetRequiredService<IOptions<CelebritiesConfig>>().Value.PhotosRequestPath;

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