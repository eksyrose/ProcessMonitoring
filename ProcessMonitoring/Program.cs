using System;
using System.Diagnostics;
using System.Threading;

namespace ProcessMonitoring
{
    class Program
    {
        private static String processName;
        private static int maxLifetimeInMinutes;
        private static void TimerCallback(object o) 
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                int processLifetimeInMinutes = (DateTime.Now - process.StartTime).Minutes;
                Console.WriteLine("Current process lifetime: " + processLifetimeInMinutes + " minute/s");
                if (processLifetimeInMinutes >= maxLifetimeInMinutes)
                {
                    process.Kill();
                    Console.WriteLine("Process " + processName + " was killed");
                }
            }
        }
        static void Main(string[] args)
        {
            Timer _timer = null;
            int monitoringFrequencyInMinutes;
            const int millisecondsInMinute = 60000;      
            try
            {
                processName = args[0]; //parsing arguments
                maxLifetimeInMinutes = Convert.ToInt32(args[1]);
                monitoringFrequencyInMinutes = Convert.ToInt32(args[2]);
                _timer = new Timer(TimerCallback, null, 0, monitoringFrequencyInMinutes * millisecondsInMinute); //converting to milliseconds
                Console.WriteLine("Program is working. Press q to exit");
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q))
                {                    
                }               
                _timer.Dispose(); //here we are closing the timer in case we will change implementation to the non-demon thread later
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Wrong data entered");            
            }
        }
    }
}

