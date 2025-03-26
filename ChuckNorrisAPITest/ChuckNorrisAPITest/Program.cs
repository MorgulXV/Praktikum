using System.Net.Http.Json;
using System.Text.Json.Serialization;

public class Joke
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class Program
{
    public static async Task Main()
    {
        using HttpClient client = new()
        {
            BaseAddress = new Uri("https://api.chucknorris.io/jokes/random")
        };
        Joke? joke = await client.GetFromJsonAsync<Joke>("");

        Console.WriteLine(joke?.Value);
    }
}