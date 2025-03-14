using NLog;
using NLog.Targets;
using NLog.Config;

namespace MyApplication{
    class Program{
            
        public static void Main(string[] args){
            var config = new LoggingConfiguration();
            var consoleTarget = new ConsoleTarget
            {
                Name = "console",
                Layout = "${level:uppercase=true}|${logger}|${message}|${stacktrace}",
            };
            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget, "*");
            LogManager.Configuration = config;
            
            Logger log = LogManager.GetCurrentClassLogger();
            
            System.Console.WriteLine("Hello World");
            log.Debug("This is a debug message");
            System.Console.WriteLine("1 or 2");
            int i = Convert.ToInt32(System.Console.ReadLine());
            if(i>2){
                log.Error(new Exception(), "Input is bigger than 2");
            }else if(i<1){
                log.Error(new Exception(), "Input is smaller than 1");
            }else if(i==1){
                log.Info("Input is 1");
            }else{
                log.Info("Input is 2");
            }
            LogManager.Shutdown();
        }     
    }
}