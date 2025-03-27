using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LotrAPIWPFProjectApp
{
    public class AppSettings
    {
        public string? ApiKey;
        public bool IsDarkMode;
        public bool StorageLimitEnabled;
        public int StorageLimit;
        public bool AllowRegexSearch;
    }


    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void CharactersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void QuotesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ApiKeyBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
            
        }
    }
}
