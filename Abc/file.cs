using System;

/*const int freezeFreshwater = 0;
const int boilFreshwater = 100;

const int freezeSaltwater = -2;
const int boilSaltwater = 102;

bool isSaltwater;
int i;*/
struct SaltWater
{
    public int temperature;

    public void checkState()
    {
        if(this.temperature <= freezeSaltwater)
        {
            System.Console.WriteLine("Water is frozen");
        }
        else if(this.temperature >= boilSaltwater)
        {
            System.Console.WriteLine("Water is steam");
        }
        else
        {
            System.Console.WriteLine("Water is fluid");
        }
    }
};


struct Freshwater
{
    public int temperature;

    public void checkState()
    {
        if(this.temperature <= freezeFreshwater)
        {
            System.Console.WriteLine("Water is frozen");
        } 
        else if(this.temperature >= boilFreshwater)
        {
            System.Console.WriteLine("Water is steam");
        }
        else
        {
            System.Console.WriteLine("Water is fluid");
        }
    }
};


       
public class Program
{
    static void checkforSaltwater() {
        int tmp1;
        bool isSaltwater;
        System.Console.WriteLine("Saltwater(1) or freshwater(0)");
        tmp1 = System.Console.Read();
        isSaltwater	= Convert.ToBoolean(tmp1);
    }
    
    
    static void requestTemperature()
    {
        while(true)
        {
         Console.WriteLine("Enter a temperature");
            int tmp = System.Console.Read();
            if(typeof(tmp) == i)
            {
                if(tmp < min)
                {
                    System.Console.WriteLine("Temperature too low, min temperature = -200");
                    continue;
                }
                else if(tmp > max)
                {
                    System.Console.WriteLine("Temperature too high, max temperature = 200");
                } 
                else
                {
                    if(isSaltwater)
                    {
                        SaltWater saltwater1;
                        saltwater.temperature = tmp;
                        break;
                    }
                    else
                    {
                        Freshwater freshwater1;
                        freshwater.temperature = tmp;
                        break;
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Input is not an int");
            }
        }
    }
    
    public static void Main(string[] args) {
       checkforSaltwater();
       requestTemperature();
       if(isSaltwater) {
        saltwater1.checkState();
       } else {
        freshwater1.checkState();
       }
    }
}

