using System;
using System.Collections.Generic;

namespace MyApplication{
    class Program{
        public static void Main(string[] args){
            List<string> list = new List<string>();
            while(true){
                Console.WriteLine("Enter a string");
                string? input = Console.ReadLine();
                if(input == ""){
                    break;
                }else{
                    list.Add(input);
                }
            }
            Console.WriteLine("The third value of the list is: " + list[2]);
            Console.ReadLine();
        }
    }
}