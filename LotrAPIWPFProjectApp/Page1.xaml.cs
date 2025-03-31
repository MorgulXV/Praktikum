using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Caching;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace LotrAPIWPFProjectApp
{
    public class CharacterCache
    {
        public static MemoryCache cache = new("CharacterCache");
        public static void Set(string key, object data, int expirationMinutes)
        {
            CacheItemPolicy policy = new()
            {
                AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration,
                SlidingExpiration = ObjectCache.NoSlidingExpiration
            };
            cache.Set(key, data, policy);
        }
        public static object Get(string key)
        {
            return cache.Get(key);
        }
    }

    public class CharacterRoot
    {
        [JsonPropertyName("docs")]
        public required CharacterDocs[] docs { get; set; }
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

    internal class CharacterDataReceiver
    {
        public async Task<CharacterRoot> ReceiveData()
        {
            HttpClient client = new();
            Random rnd = new();
            int page = rnd.Next(1, 933);
            string Url = "https://the-one-api.dev/v2/character?limit=1&page=";
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

            CharacterRoot? MyObject = JsonSerializer.Deserialize<CharacterRoot>(tmp.GetRawText(), options);
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
            CharacterDataReceiver receiver = new();
            CharacterRoot Data = await receiver.ReceiveData();
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
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            await FillCharacterData();
            StartButton.IsEnabled = true;
        }

        private void WikiUriBox_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            _ = WikiUriBox.Content is not (object)"N/A" and not (object)""
                ? System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = (string)WikiUriBox.Content,
                    UseShellExecute = true
                })
                : System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://lotr.fandom.com/wiki/Main_Page",
                    UseShellExecute = true
                });
        }

        private void QuotesButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }
    }
}

