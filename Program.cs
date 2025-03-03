using System.Text.RegularExpressions;

namespace Heartbeat
{
    class Program
    {
        private static string configOptionMatch = @"\:.+$";
        private static string? configPath = null;
        private static int? configFreqSec = null;

        public static void Main(string[] args)
        {
            // Read configuration file
            foreach (var line in File.ReadLines(".configuration"))
            {
                if (Regex.IsMatch(line, "^configPath"))
                {
                    configPath = GetConfigOptionString(line);
                }
                else if (Regex.IsMatch(line, "^configFreqSec"))
                {
                    configFreqSec = int.Parse(GetConfigOptionString(line));
                }
            }

            if (configPath != null && configFreqSec != null)
            {
                var updater = new PageUpdater(configPath);
                updater.ReadPage();

                do
                {
                    
                } while(true);
            }
        }

        private static string GetConfigOptionString(string lineIn)
        {
            return Regex.Match(lineIn, configOptionMatch).ToString().TrimStart(':');
        }
    }
}