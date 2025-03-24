using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net.Http;

namespace ChuckNorrisJokes
{
    public class Joke
    {
        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }

    public class JokeRetriever
    {
        public async Task<string> getJoke()
        {
            using HttpClient client = new()
            {
                BaseAddress = new Uri("https://api.chucknorris.io/jokes/random/")
            };
            Joke? joke = await client.GetFromJsonAsync<Joke>("");

            if (joke != null)
            {
                return joke?.Value;
            }
            else
            {
                return "No joke found";
            }
        }
    }
    public partial class MainWindow : Window
    {
        JokeRetriever jokeRetriever = new JokeRetriever();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            Text_Display.Text = "";

            Text_Display.Text = await jokeRetriever.getJoke();


            
        }
    }
}