using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Net.Http.Json; // Обязательно для ReadFromJsonAsync
using DAL_Celebrity_MSSQL;

namespace ASPA008_1.Filters
{
    public class CelebrityAsyncActionFilter
    {
        // Вложенный класс-атрибут (для использования над методами контроллера)
        public class InfoAsyncActionFilter : Attribute, IAsyncActionFilter
        {
            public static readonly string Wikipedia = "WIKI";
            public static readonly string Facebook = "FACE";
            string infotype;

            public InfoAsyncActionFilter(string infotype = "")
            {
                this.infotype = infotype;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Получаем репозиторий из сервисов запроса
                IRepository? repo = context.HttpContext.RequestServices.GetService<IRepository>();

                // Достаем ID знаменитости из аргументов метода контроллера
                if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int id)
                {
                    Celebrity? celebrity = repo?.GetCelebrityById(id);

                    if (celebrity != null)
                    {
                        // Если в параметре атрибута указана Википедия
                        if (infotype.ToUpper().Contains(Wikipedia))
                        {
                            var refs = await WikiInfoCelebrity.GetRefereces(celebrity.FullName);
                            context.HttpContext.Items.Add(Wikipedia, refs);
                        }

                        // Если в параметре атрибута указан Facebook
                        if (infotype.ToUpper().Contains(Facebook))
                        {
                            context.HttpContext.Items.Add(Facebook, getFromFace(celebrity.FullName));
                        }
                    }
                }

                await next();
            }

            string getFromFace(string fullname)
            {
                return "Info from Facebook stub for " + fullname;
            }
        }
        public class WikiInfoCelebrity
        {
            HttpClient client;
            string wikiURI;
            Dictionary<string, string> wikiReferens;

            private WikiInfoCelebrity(string fullname)
            {
                this.client = new HttpClient();
                this.wikiReferens = new Dictionary<string, string>();

                // ИСПРАВЛЕНИЕ: Используем правильное кодирование имени для URL
                string escapedName = Uri.EscapeDataString(fullname.Trim());
                this.wikiURI = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={escapedName}&limit=5&format=json";
            }

            public static async Task<Dictionary<string, string>> GetRefereces(string fullname)
            {
                WikiInfoCelebrity info = new WikiInfoCelebrity(fullname);
                info.client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                try
                {
                    HttpResponseMessage message = await info.client.GetAsync(info.wikiURI);
                    if (message.IsSuccessStatusCode)
                    {
                        var result = await message.Content.ReadFromJsonAsync<JsonElement>();

                        if (result.GetArrayLength() >= 4)
                        {
                            var names = result[1]; // Массив заголовков статей
                            var links = result[3]; // Массив ссылок

                            for (int i = 0; i < names.GetArrayLength(); i++)
                            {
                                string? key = names[i].GetString();
                                string? val = links[i].GetString();

                                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(val))
                                {
                                    info.wikiReferens.TryAdd(key, val);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wiki search failed: " + ex.Message);
                }

                return info.wikiReferens;
            }
        }
    }
}