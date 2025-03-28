using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.XPath;

namespace LotrAPIWPFProjectApp
{
    public abstract class CurrentQuoteUrl
    {
        public static string currentUrl;
    }

    public class QuoteRoot
    {
        [JsonPropertyName("docs")]
        public QuoteDocs[] docs { get; set; }
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
            var client = new HttpClient();
            var Url = "https://the-one-api.dev/v2/character/";
            var CharacterUrl = Url + id;

            var request = new HttpRequestMessage(HttpMethod.Get, CharacterUrl);
            var token = JObject.Parse(File.ReadAllText("C:\\Users\\TimHeil\\C#\\LotrAPIWPFApp\\secrets.json"))["APIKey"].ToString();

            request.Headers.Add("Authorization", token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var tmp = await response.Content.ReadFromJsonAsync<JsonElement>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var MyObject = JsonSerializer.Deserialize<CharacterRoot>(tmp.GetRawText(), options);
            return MyObject.docs[0].name;
        }
    }

    public class QuoteDataReceiver
    {
        public async Task<QuoteRoot> ReceiveData()
        {
            var client = new HttpClient();
            Random rnd = new Random();
            int page = rnd.Next(1, 2384);
            var Url = "https://the-one-api.dev/v2/quote?limit=1&page=";
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

            var MyObject = JsonSerializer.Deserialize<QuoteRoot>(tmp.GetRawText(), options);
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
            var receiver = new QuoteDataReceiver();
            var Data = await receiver.ReceiveData();
            var characterReceiver = new CharacterNameReceiver();

            if (Data.docs.Length > 0)
            {
                QuoteBox1.Text = Data.docs[0].dialog ?? "N/A";
                QuotedFromBox1.Text = "Quote by: " + await characterReceiver.NameReceiver(Data.docs[0].character) ?? "N/A";
            }
            
        }

        private void CharactersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RandomQuoteButton.IsEnabled = false;
            await FillQuoteData();
            RandomQuoteButton.IsEnabled = true;
        }
    }
}
