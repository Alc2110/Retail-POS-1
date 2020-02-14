using System;
using NLog;
using System.Reflection;
using System.IO;

namespace POS
{
    public static class Configuration
    {
        public enum Role
        {
            NORMAL,
            ADMIN
        }

        public static Role userLevel;
        public static string connectionString = "Server=localhost;Database=Retail_POS;Trusted_Connection=Yes";

        public static class Logger
        {
            public static void ConfigureLogger()
            {
                var config = new NLog.Config.LoggingConfiguration();

                // target where to log to
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = path + @"\log.txt" };

                // rules for mapping loggers to targets
                // minimum and maximum log levels for logging targets
                config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

                // apply config
                NLog.LogManager.Configuration = config;
        }
    }
}