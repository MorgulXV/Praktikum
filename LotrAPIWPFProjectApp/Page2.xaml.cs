using LotrAPIWPFApp;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LotrAPIWPFProjectApp
{
    public class QuoteRoot
    {
        [JsonPropertyName("docs")]
        public required QuoteDocs[] docs { get; set; }
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

    public class QuoteDocs
    {
        [JsonPropertyName("_id")]
        public string? _id { get; set; }
        [JsonPropertyName("dialog")]
        public string? dialog { get; set; }
        [JsonPropertyName("movie")]
        public string? movie { get; set; }
        [JsonPropertyName("character")]
        public string? character { get; set; }
    }

    public class CharacterNameReceiver
    {   

        public async Task<string> NameReceiver(string id)
        {
            HttpClient client = new();
            string Url = "https://the-one-api.dev/v2/character/";
            string CharacterUrl = Url + id;

            HttpRequestMessage request = new(HttpMethod.Get, CharacterUrl);
            string token = JObject.Parse(File.ReadAllText("C:\\Users\\TimHeil\\C#\\LotrAPIWPFApp\\secrets.json"))["APIKey"].ToString();

            request.Headers.Add("Authorization", token);
            HttpResponseMessage response = await client.SendAsync(request);
            _ = response.EnsureSuccessStatusCode();
            JsonElement tmp = await response.Content.ReadFromJsonAsync<JsonElement>();

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            CharacterRoot? MyObject = JsonSerializer.Deserialize<CharacterRoot>(tmp.GetRawText(), options);
            return MyObject.docs[0].name;
        }
    }

    public class QuoteDataReceiver
    {
        public async Task<QuoteRoot> ReceiveData()
        {
            HttpClient client = new();
            Random rnd = new();
            int page = rnd.Next(1, 2384);
            string Url = "https://the-one-api.dev/v2/quote?limit=1&page=";
            string RandomUrl = Url + page;
            HttpRequestMessage request = new(HttpMethod.Get, RandomUrl);
            string token = JObject.Parse(File.ReadAllText("C:\\Users\\TimHeil\\C#\\LotrAPIWPFApp\\secrets.json"))["APIKey"].ToString();

            request.Headers.Add("Authorization", token);
            HttpResponseMessage response = await client.SendAsync(request);
            _ = response.EnsureSuccessStatusCode();
            JsonElement tmp = await response.Content.ReadFromJsonAsync<JsonElement>();
            Console.WriteLine(tmp);

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            QuoteRoot? MyObject = JsonSerializer.Deserialize<QuoteRoot>(tmp.GetRawText(), options);
            return MyObject;
        }
    }


    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        public async Task FillQuoteData()
        {
            QuoteDataReceiver receiver = new();
            QuoteRoot Data = await receiver.ReceiveData();
            CharacterNameReceiver characterReceiver = new();

            if (Data.docs.Length > 0)
            {
                QuoteBox1.Text = Data.docs[0].dialog ?? "N/A";
                QuotedFromBox1.Text = "Quote by: " + await characterReceiver.NameReceiver(Data.docs[0].character) ?? "N/A";
            }

        }

        private void CharactersButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RandomQuoteButton.IsEnabled = false;
            await FillQuoteData();
            RandomQuoteButton.IsEnabled = true;
        }
    }
}
