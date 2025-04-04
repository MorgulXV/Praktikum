using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyApplication
{
    public class Root
    {
        [JsonPropertyName("docs")]
        public Docs[] docs { get; set; }
        [JsonPropertyName("total")]
        public int total { get; set; }
        [JsonPropertyName("limit")]
        public int limit { get; set; }
        [JsonPropertyName("offset")]
        public int offset { get; set; }
        [JsonPropertyName("page")]
        public int page { get; set; }
        [JsonPropertyName("pages")]
        public int pages { get; set; }
    }

    public class Docs
    {
        [JsonPropertyName("_id")]
        public string? _id { get; set; }
        [JsonPropertyName("name")]
        public string? name { get; set; }
        [JsonPropertyName("wikiUrl")]
        public string? wikiUrl { get; set; }
        [JsonPropertyName("race")]
        public string? race { get; set; }
        [JsonPropertyName("birth")]
        public string? birth { get; set; }
        [JsonPropertyName("gender")]
        public string? gender { get; set; }
        [JsonPropertyName("death")]
        public string? death { get; set; }
        [JsonPropertyName("hair")]
        public string? hair { get; set; }
        [JsonPropertyName("height")]
        public string? height { get; set; }
        [JsonPropertyName("realm")]
        public string? realm { get; set; }
        [JsonPropertyName("spouse")]
        public string? spouse { get; set; }

    }
    class Program{
        public static async Task Main()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://the-one-api.dev/v2/character?name=Gandalf");
            request.Headers.Add("Authorization", "API Key");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var tmp = await response.Content.ReadFromJsonAsync<JsonElement>();
            Console.WriteLine(tmp);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var MyObject = JsonSerializer.Deserialize<Root>(tmp.GetRawText(), options);

            if (MyObject?.docs != null)
            {
                Console.WriteLine(MyObject.docs[0].name);
                Console.WriteLine(MyObject.docs[0].race);
                Console.WriteLine(MyObject.docs[0].birth);
                Console.WriteLine(MyObject.docs[0].gender);
                    
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.ReadLine();
        }
    }
}