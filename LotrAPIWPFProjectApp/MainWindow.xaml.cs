using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using LotrAPIWPFProjectApp;


namespace LotrAPIWPFApp
{
    public abstract class CurrentUrl
    {
        public static string currentUrl;
    }
    
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

    class Receiver
    {
        public async Task<Root> ReceiveData()
        {
            var client = new HttpClient();
            Random rnd = new Random();
            int page = rnd.Next(1, 933);
            var Url = "https://the-one-api.dev/v2/character?limit=1&page=";
            var RandomUrl = Url + page;
            var request = new HttpRequestMessage(HttpMethod.Get, RandomUrl);
            var token = JObject.Parse(File.ReadAllText("C:\\Users\\TimHeil\\C#\\LotrAPIWPFApp\\secrets.json"))["APIKey"].ToString();

            request.Headers.Add("Authorization", token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var tmp = await response.Content.ReadFromJsonAsync<JsonElement>();
            Console.WriteLine(tmp);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var MyObject = JsonSerializer.Deserialize<Root>(tmp.GetRawText(), options);
            return MyObject;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1());
        }
      
    }
}