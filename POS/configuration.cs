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

        public static int STAFF_ID;
        public static Role USER_LEVEL;

        //public static string CONNECTION_STRING = "Server=localhost;Database=Retail_POS;Trusted_Connection=Yes";
        public static string CONNECTION_STRING;

        //public static string CONNECTION_STRING = "Server=192.168.56.1;Database=Retail_POS;Trusted_Connection=Yes";

        public static string STORE_NAME;

        public static string VERSION = "0.1";

        public static class SpreadsheetConstants
        {
            public static int SPREADHSEET_ROW_OFFSET = 7;
            public static int SPREADSHEET_HEADER_ROW = SPREADHSEET_ROW_OFFSET - 1;
        }

        public static class Logger
        {
            public static void ConfigureLogger()
            {
                var config = new NLog.Config.LoggingConfiguration();

                // target where to log to
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                // to a logfile
                // keep it open to increase performance
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = path + @"\log.txt", KeepFileOpen=true, OpenFileCacheTimeout=5 };

                // rules for mapping loggers to targets
                // minimum and maximum log levels for logging targets
                config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

                // apply config
                NLog.LogManager.Configuration = config;            
            }
        }
    }
}