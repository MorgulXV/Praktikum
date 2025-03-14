using System.Reflection.PortableExecutable;
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

namespace CoffeeMachineWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    interface IMachine
    {
        bool getIsOn();
        bool switchOn();
        bool switchOff();
    }
    
    class CoffeeMachine : IMachine
    {
        private bool isOn;
        private bool isBrewing;
        private bool hasCoffeePowder;
        private int remainingCups;
        private bool hasWater;

        public CoffeeMachine()
        {
            this.isOn = false;
            this.isBrewing = false;
            this.hasCoffeePowder = false;
            this.remainingCups = 0;
            this.hasWater = false;
        }

        public bool brew()
        {
            if (this.isOn && this.hasCoffeePowder && this.hasWater && !(this.isBrewing) && (remainingCups == 0))
            {
                this.isBrewing = true;
                Thread.Sleep(10000);
                this.isBrewing = false;
                this.remainingCups = 5;
                this.hasCoffeePowder = false;
                this.hasWater = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void takeCup()
        {
            if (remainingCups > 0)
            {
                remainingCups--;
            }
            else
            {
                remainingCups = 0;
            }
        }

        public void refillCoffee()
        {
            this.hasCoffeePowder = true;
        }
        public void refillWater()
        {
            this.hasWater = true;
        }
        public bool switchOn()
        {
            if (!(this.isOn))
            {
                this.isOn = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool switchOff()
        {
            if (isOn)
            {
                this.isOn = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool getIsOn()
        {
            return this.isOn;
        }
        public int getRemainingCups()
        {
            return this.remainingCups;
        }
        public bool getHasCoffeePowder()
        {
            return this.hasCoffeePowder;
        }

        public bool getHasWater()
        {
            return this.hasWater;
        }
    }

    public partial class MainWindow : Window
    {
        CoffeeMachine NewCoffeeMachine = new CoffeeMachine();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Power_Switch_Clicked(object sender, RoutedEventArgs e)
        {
            if (NewCoffeeMachine.getIsOn()){
                NewCoffeeMachine.switchOff();
                StatusLED.Fill = new SolidColorBrush(Colors.Red);
                Power_Switch.IsChecked = false;
            }
            else
            {
                NewCoffeeMachine.switchOn();
                StatusLED.Fill = new SolidColorBrush(Colors.LightGreen);
                Power_Switch.IsChecked = true;
            }
            
        }

        private void Refill_Water_Button_Click_(object sender, RoutedEventArgs e)
        {
            NewCoffeeMachine.refillWater();
            Water_Fill_Level.Value = 1;
        }

        private void Refill_Coffee_Button_Click(object sender, RoutedEventArgs e)
        {
            NewCoffeeMachine.refillCoffee();
            Coffee_Powder_Fill_Level.Value = 1;
        }

        private void Take_Cup_Button_Click_(object sender, RoutedEventArgs e)
        {
            NewCoffeeMachine.takeCup();
            Remaining_Cups_Fill_Level.Value = NewCoffeeMachine.getRemainingCups();

        }

        private void Brew_Coffee_Button_Click(object sender, RoutedEventArgs e)
        {
            NewCoffeeMachine.brew();
            Water_Fill_Level.Value = Convert.ToInt32(NewCoffeeMachine.getHasWater());
            Coffee_Powder_Fill_Level.Value = Convert.ToInt32(NewCoffeeMachine.getHasCoffeePowder());
            Remaining_Cups_Fill_Level.Value = NewCoffeeMachine.getRemainingCups();
        }
    }
}