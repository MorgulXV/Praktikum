//TODO 
// Fix get/set properties of temperature
//Use a linter
//Use bitfields for temperature(optimization)
  
namespace MyApplication
{
    class Water
    {
        private enum Defaults{
            defaultTemperature = 20,
            defaultSalt = 0,
            freshwaterBoilingPoint = 100,
            saltwaterBoilingPoint = 102,
            freshwaterFreezingPoint = 0,
            saltwaterFreezingPoint = -2,
            maxInput = 200,
            minInput = -200
        }
        private int _temperature;
        public int Temperature
        {
            get => _temperature;
            set
            {
                if((value >= (int)Defaults.minInput) && (value <= (int)Defaults.maxInput) && (value.GetType() == typeof(int))){
                    _temperature = value;
                }else{
                    throw new Exception();
                }
            }
        }
        private bool isSaltwater; 

        public Water(): this((int)Defaults.defaultTemperature, Convert.ToBoolean((int)Defaults.defaultSalt)){}
        public Water(bool isSaltwater): this((int)Defaults.defaultTemperature, isSaltwater){}
        public Water(int temperature, bool isSaltwater)
        {
            this.Temperature = temperature;
            this.isSaltwater = isSaltwater;
        }

        public bool isHot(int temperature, bool isSaltwater){
            if(isSaltwater && (temperature >= (int)Defaults.saltwaterBoilingPoint)){
                return true;
            }else if((!isSaltwater) && temperature >= (int)Defaults.freshwaterBoilingPoint) {
                return true;
            } else {
                return false;
            }
        }

        public bool isCold(int temperature, bool isSaltwater){
            if(isSaltwater && (temperature <= (int)Defaults.saltwaterFreezingPoint)){
                return true;
            }else if((!isSaltwater) && temperature <= (int)Defaults.freshwaterFreezingPoint) {
                return true;
            } else {
                return false;
            }
        }


        public static void Main(string[] args) {
            
            Water Saltwater = new Water();
            Water Freshwater = new Water(false);
        }
    }
}