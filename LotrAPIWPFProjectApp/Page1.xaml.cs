using LotrAPIWPFApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LotrAPIWPFProjectApp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        public async Task FillData()
        {
            var receiver = new Receiver();
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
            CurrentUrl.currentUrl = Data.docs[0].wikiUrl ?? "N/A";

        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            await FillData();
            StartButton.IsEnabled = true;
        }

        private void WikiUriBox_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if(!(CurrentUrl.currentUrl.Equals("N/A") || CurrentUrl.currentUrl.Equals("")))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = CurrentUrl.currentUrl,
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
    }
}
