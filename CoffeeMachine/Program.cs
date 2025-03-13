using System;

namespace MyApplication
{
    interface IMachine{
        bool getIsOn();
        bool switchOff();
        bool switchOn();
    }
    class CoffeeMachine : IMachine{
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

        public bool brew(){
            if(this.isOn && this.hasCoffeePowder && this.hasWater && !(this.isBrewing) && (remainingCups == 0)){
                this.isBrewing = true;
                this.isBrewing = false;
                this.remainingCups = 5;
                this.hasCoffeePowder = false;
                this.hasWater = false;
                return true;
            }else{
                return false;
            }
        }

        public bool takeCup(){
            if(remainingCups > 0){
                remainingCups--;
                return true;
            }else{
                return false;
            }
        }

        public bool refillCoffee(){
            if(this.hasCoffeePowder == false){
                this.hasCoffeePowder = true;
                return true;
            }else{
                return false;
            }
        }
        public bool refillWater(){
            if(this.hasWater == false){
                this.hasWater = true;
                return true;
            }else{
                return false;
            }
        }
        public bool switchOn(){
            if(!(this.isOn)){
                this.isOn = true;
                return true;
            }else{
                return false;
            }
        }
        public bool switchOff(){
            if(!(isOn)){
                this.isOn = false;
                return true;
            }else{
                return false;
            }
        }

        public bool getIsOn(){
            if(this.isOn){
                return true;
            }else{
                return false;
            }
        }
        public int getRemainingCups(){
            return this.remainingCups;
        }
        public bool getHasCoffeePowder(){
            return this.hasCoffeePowder;
        }

        public bool getHasWater(){
            return this.hasWater;
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
                            if(NewCoffeeMachine.switchOn()){
                                 Console.WriteLine("Coffee machine switched on");
                            }else{
                                Console.WriteLine("Coffee machine already switched on");
                            }
                            break;
                        case 2:
                            if(NewCoffeeMachine.refillWater()){
                                Console.WriteLine("Water has been refilled");
                            }else{
                                Console.WriteLine("The water has already been refilled");
                            }
                            break;
                        case 3:
                            if(NewCoffeeMachine.refillCoffee()){
                                Console.WriteLine("Coffee powder has been refilled");
                            }else{
                                Console.WriteLine("Coffee powder has already been refilled");
                            }
                            break;
                        case 4:
                            if(NewCoffeeMachine.brew()){
                                System.Console.WriteLine("Started brewing");
                                System.Threading.Thread.Sleep(5000);
                                Console.WriteLine("Stopped brewing");
                            }else if(NewCoffeeMachine.getIsOn() == false){
                                System.Console.WriteLine("Coffee machine isn't on");
                            }else if(NewCoffeeMachine.getHasCoffeePowder() == false){
                                System.Console.WriteLine("No coffee powder availiable, please refill");
                            }else if(NewCoffeeMachine.getHasWater() == false){
                                Console.WriteLine("No water available, please refill");
                            }else if(NewCoffeeMachine.getRemainingCups() > 0){
                                System.Console.WriteLine("You have " + NewCoffeeMachine.getRemainingCups() + " more cups to take");
                            }
                            break;
                        case 5:
                            if(NewCoffeeMachine.takeCup()){
                                Console.WriteLine("Cup has been taken");
                            }else{
                                Console.WriteLine("Coffee machine empty, please brew new coffee");
                            }
                            break;
                        case 6:
                            if(NewCoffeeMachine.switchOff() == true){
                                Console.WriteLine("Coffee machine switched off");
                            }else{
                                Console.WriteLine("Coffee machine has already been switched off");
                            }
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