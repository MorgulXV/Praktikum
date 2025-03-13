using System;

namespace MyApplication
{
    class CoffeeMachine{
        private bool isOn;
        private bool isBrewing;
        private bool hasCoffeePowder;
        private int remainingCups;
        private bool hasWater;

        public CoffeeMachine(){
            this.isOn = false;
            this.isBrewing = false;
            this.hasCoffeePowder = false;
            this.remainingCups = 0;
            this.hasWater = false;
        }

        public void brew(){
            if(this.isOn && this.hasCoffeePowder && this.hasWater && !(this.isBrewing) && (remainingCups == 0)){
                System.Console.WriteLine("Started brewing");
                this.isBrewing = true;
                System.Threading.Thread.Sleep(5000);
                this.isBrewing = false;
                Console.WriteLine("Stopped brewing");
                this.remainingCups = 5;
                this.hasCoffeePowder = false;
                this.hasWater = false;
            }else if(!(this.isOn)){
                System.Console.WriteLine("Coffee machine isn't on");
            }else if(!(this.hasCoffeePowder)){
                System.Console.WriteLine("No coffee powder availiable, please refill");
            }else if(!(hasWater)){
                Console.WriteLine("No water available, please refill");
            }else if(remainingCups > 0){
                System.Console.WriteLine("You have " + remainingCups + " more cups to take");
            }
        }

        public void takeCup(){
            if(remainingCups > 0){
                remainingCups--;
                System.Console.WriteLine("Cup Taken");
            }else{
                System.Console.WriteLine("Coffee machine empty please brew new coffee");
            }
        }

        public void refillCoffee(){
            if(this.hasCoffeePowder == false){
                this.hasCoffeePowder = true;
                System.Console.WriteLine("Coffe powder has been refilled");
            }else{
                System.Console.WriteLine("Coffee powder already has been refilled");
            }
        }
        public void refillWater(){
            if(this.hasWater == false){
                this.hasWater = true;
                System.Console.WriteLine("Water has been refilled");
            }else{
                System.Console.WriteLine("Water already has been refilled");
            }
        }
        public void switchOn(){
            if(isOn){
                Console.WriteLine("Machine is already on");
            }else{
                this.isOn = true;
            }
        }
        public void switchOff(){
            if(!isOn){
                Console.WriteLine("Machine is already off");
            }else{
                this.isOn = false;
            }
        }
    }

    class Program{
        public static void Main(string[] args){
            CoffeeMachine NewCoffeeMachine = new CoffeeMachine();
            int? choice;
            while(true){
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Start coffee machine");
                Console.WriteLine("2. Refill Water");
                Console.WriteLine("3. Refill coffee powder");
                Console.WriteLine("4. Brew coffe");
                Console.WriteLine("5. Take a cup");
                Console.WriteLine("6. Stop machine");
                Console.WriteLine("7. Exit");
                choice =  Convert.ToInt32(Console.ReadLine());
                if(choice !=null){
                    switch(choice){
                        case 1:
                            NewCoffeeMachine.switchOn();
                            break;
                        case 2:
                            NewCoffeeMachine.refillWater();
                            break;
                        case 3:
                            NewCoffeeMachine.refillCoffee();
                            break;
                        case 4:
                            NewCoffeeMachine.brew();
                            break;
                        case 5:
                            NewCoffeeMachine.takeCup();
                            break;
                        case 6:
                            NewCoffeeMachine.switchOff();
                            break;
                        case 7:
                            Environment.Exit(1);
                            break;
                    }
                }else{
                    continue;
                }
            }
        }
    }
}