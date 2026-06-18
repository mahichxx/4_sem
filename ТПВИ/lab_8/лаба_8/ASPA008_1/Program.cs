using DAL_Celebrity_MSSQL;
using static ANC25_WEBAPI_DLL.Services.CelebritiesAPIExtensions;
using static ANC25_WEBAPI_DLL.Services.CelebrityAPI;
using static ANC25_WEBAPI_DLL.Services.MiddlewareErrorHandler;
using ANC25_WEBAPI_DLL.Services;
using Microsoft.AspNetCore.Builder;

internal class Program
{
	private static void Main(string[] args)
	{
        var builder = WebApplication.CreateBuilder(args);

        // 1. Подключение конфигурации (из Celebrities.config.json)
        builder.AddCelebritiesConfiguration();

        // 2. Подключение сервисов (IRepository, CountryCodes и т.д.)
        builder.AddCelebritiesServices();

        // 3. Если вы хотите использовать WikiInfoCelebrity как отдельный сервис (рекомендуется):
        builder.Services.AddScoped<WikiInfoCelebrity>();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseHttpsRedirection();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        // 1. Добавляем маршрутизацию
        app.UseRouting();

        // 2. Обработчик ошибок (проверьте имя метода и нужен ли параметр "ANC28")
        app.UseASPErrorHandler();

        // 3. Маппинг API (должен быть ПОСЛЕ UseRouting)
        app.MapCelebrities();
        app.MapLifeevents();
        app.MapPhotoCelebrities();

        app.UseAuthorization();

        // Для создания новой (поиск "бриллианта")
        app.MapControllerRoute(
            name: "new_celebrity", // Было "celebrity"
            pattern: "0",
            defaults: new { controller = "Celebrities", Action = "NewHumanForm" });

        // Для детальной страницы (ваш случай /3)
        app.MapControllerRoute(
            name: "human_details", // Было "celebrity"
            pattern: "{id:int:min(1)}",
            defaults: new { controller = "Celebrities", Action = "Human" });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Celebrities}/{action=Index}/{id?}");

        app.Run();
    }
}