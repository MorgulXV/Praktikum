using LotrAPIWPFApp;
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

    public partial class Page1 : Page
    {   
        public Page1()
        {
            InitializeComponent();
        }
        public async Task FillCharacterData()
        {
            Random rnd = new();
            int index = rnd.Next(0, CharacterCache.characterCache.Count-1);
            NameBox.Text = CharacterCache.characterCache[index].name ?? "N/A";
            RaceBox.Text = CharacterCache.characterCache[index].race ?? "N/A";
            BirthBox.Text = CharacterCache.characterCache[index].birth ?? "N/A";
            GenderBox.Text = CharacterCache.characterCache[index].gender ?? "N/A";
            DeathBox.Text = CharacterCache.characterCache[index].death ?? "N/A";
            HairBox.Text = CharacterCache.characterCache[index].hair ?? "N/A";
            HeightBox.Text = CharacterCache.characterCache[index].height ?? "N/A";
            RealmBox.Text = CharacterCache.characterCache[index].realm ?? "N/A";
            SpouseBox.Text = CharacterCache.characterCache[index].spouse ?? "N/A";
            WikiUriBox.Content = HttpUtility.UrlDecode(CharacterCache.characterCache[index].wikiUrl ?? "N/A");
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

