using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessMonitoring
{
    class Program
    {
        private static Timer _timer = null;
        private static String[] subs;
        private static String processName;
        private static int maxLifetime;
        private static int monitoringFrequiency;
        private static void TimerCallback(object o)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                int processLifetime = (DateTime.Now - process.StartTime).Minutes;
                Console.WriteLine("Current process lifetime: " + processLifetime + " minute/s");
                if (processLifetime > maxLifetime)
                {
                    process.Kill();
                    Console.WriteLine("Process " + processName + " was killed");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter process name, its maximum lifetime in minutes and a monitoring frequency in minutes. Example: notepad 5 1");
            String line = Console.ReadLine();         
            try
            {
                subs = line.Split(); //parsing arguments
                processName = subs[0];
                maxLifetime = Convert.ToInt32(subs[1]);
                monitoringFrequiency = Convert.ToInt32(subs[2]);
                _timer = new Timer(TimerCallback, null, 0, monitoringFrequiency * 60000); //converting to miliseconds
                Console.WriteLine("Program is working. Press q to exit");
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q))
                {                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong data");
                return;
            }           
        }
    }
}
