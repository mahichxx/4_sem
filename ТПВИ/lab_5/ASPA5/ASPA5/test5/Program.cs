using System.Net.Http.Json;
using System.Text.Json;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

Test test = new Test();
string baseUrl = "http://localhost:5116";

//a
Console.WriteLine("/A");
await test.ExecuteGET<int?>($"{baseUrl}/A/3", (x, y, s) => (x == 3 && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<int?>($"{baseUrl}/A/-3", (x, y, s) => (x == -3 && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<int?>($"{baseUrl}/A/118", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{baseUrl}/A/5", (x, y, s) => (x == 5 && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{baseUrl}/A/-5", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{baseUrl}/A/118", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{baseUrl}/A/2/3", (x, y, s) => (x == 2 && y == 3 && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{baseUrl}/A/0/3", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{baseUrl}/A/25/-3", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{baseUrl}/A/0/-3", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{baseUrl}/A/1-99", (x, y, s) => (x == 1 && y == 99 && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{baseUrl}/A/99-1", (x, y, s) => (x == 99 && y == 1 && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{baseUrl}/A/-1-25", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{baseUrl}/A/1--25", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{baseUrl}/A/25-101", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);

//b
Console.WriteLine("/B");
await test.ExecuteGET<float?>($"{baseUrl}/B/2.5", (x, y, s) => (x == 2.5f && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<float?>($"{baseUrl}/B/2", (x, y, s) => (x == 2.0f && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<float?>($"{baseUrl}/B/2X", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<float?>($"{baseUrl}/B/2.5/3.2", (x, y, s) => (x == 2.5f && y == 3.2f && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<float?>($"{baseUrl}/B/2.5-3.2", (x, y, s) => (x == 2.5f && y == 3.2f && s == 200) ? Test.OK : Test.NOK);

//c
Console.WriteLine("/C");
await test.ExecuteGET<bool?>($"{baseUrl}/C/2.5", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<bool?>($"{baseUrl}/C/true", (x, y, s) => (x == true && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<bool?>($"{baseUrl}/C/true,false", (x, y, s) => (x == true && y == false && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<bool?>($"{baseUrl}/C/true,false", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);

//d
Console.WriteLine("/D");
await test.ExecuteGET<DateTime?>($"{baseUrl}/D/2025-02-25", (x, y, s) => (x == new DateTime(2025, 02, 25) && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{baseUrl}/D/2025-02-29", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{baseUrl}/D/2024-02-29", (x, y, s) => (x == new DateTime(2024, 02, 29) && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{baseUrl}/D/2025-02-25T19:25", (x, y, s) => (x == new DateTime(2025, 02, 25, 19, 25, 0) && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<DateTime?>($"{baseUrl}/D/2025-02-25|2025-03-25", (x, y, s) => (x == new DateTime(2025, 02, 25) && y == new DateTime(2025, 03, 25) && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<DateTime?>($"{baseUrl}/D/2025-02-25T19:25", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);

//e
Console.WriteLine("/E");
await test.ExecuteGET<string?>($"{baseUrl}/E/12-bis", (x, y, s) => (x == "bis" && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/E/11-bis", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/E/12-777", (x, y, s) => (x == "777" && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/E/12-", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{baseUrl}/E/abcd", (x, y, s) => (x == "abcd" && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{baseUrl}/E/abcd123", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{baseUrl}/E/a", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{baseUrl}/E/123456", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{baseUrl}/E/aabbccddeeffgghh", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);

//f
Console.WriteLine("/F");
await test.ExecuteGET<string?>($"{baseUrl}/F/smw@belstu.by", (x, y, s) => (x == "smw@belstu.by" && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/F/xxx@yyyy.by", (x, y, s) => (x == "xxx@yyyy.by" && y == null && s == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/F/xxx@yyyy.ru", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/F/xxxyyyy.by", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{baseUrl}/F/xxx@yyyy", (x, y, s) => (x == null && y == null && s == 404) ? Test.OK : Test.NOK);

class Test
{
    class Answer<T>
    {
        public T? x { get; set; } = default;
        public T? y { get; set; } = default;
        public string? message { get; set; } = null;
    }
    public static string OK = "OK ", NOK = "NOK";
    HttpClient client = new HttpClient();

    public async Task ExecuteGET<T>(string path, Func<T?, T?, int, string> result) =>
        await resultPRINT<T>("GET", path, await client.GetAsync(path), result);

    public async Task ExecutePOST<T>(string path, Func<T?, T?, int, string> result) =>
        await resultPRINT<T>("POST", path, await client.PostAsync(path, null), result);

    public async Task ExecutePUT<T>(string path, Func<T?, T?, int, string> result) =>
        await resultPRINT<T>("PUT", path, await client.PutAsync(path, null), result);

    public async Task ExecuteDELETE<T>(string path, Func<T?, T?, int, string> result) =>
        await resultPRINT<T>("DELETE", path, await client.DeleteAsync(path), result);

    async Task resultPRINT<T>(string method, string path, HttpResponseMessage rm, Func<T?, T?, int, string> result)
    {
        int status = (int)rm.StatusCode;
        try
        {
            var answer = await rm.Content.ReadFromJsonAsync<Answer<T>>();
            T? x = default, y = default;
            string r = result(x, y, status);
            if (answer != null)
            {
                x = answer.x; y = answer.y;
                r = result(x, y, status);
            }
            Console.WriteLine($"{r}: {method} {path}, status = {status}, x = {x}, y = {y}, m = {answer?.message}");
        }
        catch (JsonException ex)
        {
            string r = result(default, default, status);
            Console.WriteLine($"{r}: {method} {path}, status = {status}, x = {null}, y = {null}, m = {ex.Message}");
        }
    }
}