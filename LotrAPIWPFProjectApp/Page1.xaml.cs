using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System;
using System.Runtime.Caching;


namespace LotrAPIWPFProjectApp
{
    public class CharacterCache
    {
        public static MemoryCache cache = new MemoryCache("CharacterCache");
        public static void Set(string key, object data, int expirationMinutes)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationMinutes)
            };
            cache.Set(key, data, policy);
        }
        public static object Get(string key) 
        { 
            return cache.Get(key);
        }
    }

    public abstract class CurrentCharacterUrl
    {
        public static string currentUrl;
    }

    public class CharacterRoot
    {
        [JsonPropertyName("docs")]
        public CharacterDocs[] docs { get; set; }
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

    public class CharacterDocs
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

    class CharacterDataReceiver
    {
        public async Task<CharacterRoot> ReceiveData()
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

            var MyObject = JsonSerializer.Deserialize<CharacterRoot>(tmp.GetRawText(), options);
            return MyObject;
        }
    }
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        public async Task FillCharacterData()
        {
            var receiver = new CharacterDataReceiver();
            var Data = await receiver.ReceiveData();
            NameBox.Text = Data.docs[0].name ?? "N/A";
            RaceBox.Text = Data.docs[0].race ?? "N/A";
            BirthBox.Text = Data.docs[0].birth ?? "N/A";
            GenderBox.Text = Data.docs[0].gender ?? "N/A";
            DeathBox.Text = Data.docs[0].death ?? "N/A";
            HairBox.Text = Data.docs[0].hair ?? "N/A";
            HeightBox.Text = Data.docs[0].height ?? "N/A";
            RealmBox.Text = Data.docs[0].realm ?? "N/A";
            SpouseBox.Text = Data.docs[0].spouse ?? "N/A";
            WikiUriBox.Content = HttpUtility.UrlDecode(Data.docs[0].wikiUrl ?? "N/A");
            CurrentCharacterUrl.currentUrl = Data.docs[0].wikiUrl ?? "N/A";

        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            await FillCharacterData();
            StartButton.IsEnabled = true;
        }

        private void WikiUriBox_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if(!(CurrentCharacterUrl.currentUrl.Equals("N/A") || CurrentCharacterUrl.currentUrl.Equals("")))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = CurrentCharacterUrl.currentUrl,
                    UseShellExecute = true
                });
            }
            else
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://lotr.fandom.com/wiki/Main_Page",
                    UseShellExecute = true
                });
            }
            
        }

        private void QuotesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }
    }
}
