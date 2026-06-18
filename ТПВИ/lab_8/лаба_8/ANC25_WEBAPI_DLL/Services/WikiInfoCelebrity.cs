using System.Net.Http.Json;
using System.Text.Json;

namespace ANC25_WEBAPI_DLL.Services
{
    public class WikiInfoCelebrity
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<Dictionary<string, string>> GetRefereces(string fullname)
        {
            var wikiRefereces = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(fullname)) return wikiRefereces;

            string escapedName = Uri.EscapeDataString(fullname.Trim());

            string url = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={escapedName}&limit=5&format=json";

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Wikipedia возвращает массив: [запрос, [имена], [описания], [ссылки]]
                    var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                    if (json.ValueKind == JsonValueKind.Array && json.GetArrayLength() >= 4)
                    {
                        var names = json[1];
                        var links = json[3];

                        for (int i = 0; i < names.GetArrayLength(); i++)
                        {
                            string? name = names[i].GetString();
                            string? link = links[i].GetString();
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(link))
                            {
                                wikiRefereces.TryAdd(name, link);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Wiki Error: {ex.Message}"); }

            return wikiRefereces;
        }
    }
}