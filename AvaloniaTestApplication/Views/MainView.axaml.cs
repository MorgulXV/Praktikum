using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace AvaloniaTestApplication.Views;

public partial class MainView : UserControl
{
    public MainView() => InitializeComponent();
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Click! Celsius={Celsius.Text}");
        if (double.TryParse(Celsius.Text, out double C))
        {
            var F = C * 9 / 5 + 32;
            Fahrenheit.Text = F.ToString("0.0");
        }
        else
        {
            Fahrenheit.Text = "0";
            Celsius.Text = "0";
        }
    }
    private void CelsiusChanged(object? sender, TextChangedEventArgs e)
    {
       Debug.WriteLine($"Click! Celsius={Celsius.Text}");
        if (double.TryParse(Celsius.Text, out double C))
        {
            var F = C * 9 / 5 + 32;
            Fahrenheit.Text = F.ToString("0.0");
        }
        else
        {
            Fahrenheit.Text = "0";
            Celsius.Text = "0";
        }
    }
}
