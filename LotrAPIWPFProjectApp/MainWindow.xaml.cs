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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillCache();
            MainFrame.Navigate(new Page1());
        }

        private async Task FillCache()
        {
            // Fill the cache
            CharacterDataReceiver receiver = new();
            CharacterRoot Data = await receiver.ReceiveData();
            CharacterCache.characterCache = Data.docs;
        }

    }
    internal class CharacterDataReceiver
    {
        public async Task<CharacterRoot> ReceiveData()
        {
            HttpClient client = new();
            string Url = "https://the-one-api.dev/v2/character/";
            HttpRequestMessage request = new(HttpMethod.Get, Url);
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
}
