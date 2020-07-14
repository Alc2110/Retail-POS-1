using System;
using NLog;
using System.Reflection;
using System.IO;

namespace POS
{
    public static class Configuration
    {
        // program state
        public static Model.ProgramState currentProgramState;

        // models
        public static Model.ServiceLayer.CustomerOps customerOps;
        public static Model.ServiceLayer.ProductOps productOps;
        public static Model.ServiceLayer.StaffOps staffOps;
        public static Model.ServiceLayer.TransactionOps transactionOps;

        // current user information
        public enum Role
        {
            NORMAL,
            ADMIN
        }
        public static int STAFF_ID;
        public static Role USER_LEVEL;

        public static string CONNECTION_STRING;

        public static string STORE_NAME;

        public static string VERSION = "0.1.2";

        public static class SpreadsheetConstants
        {
            public static int SPREADHSEET_ROW_OFFSET = 7;
            public static int SPREADSHEET_HEADER_ROW = SPREADHSEET_ROW_OFFSET - 1;
        }

        public static class ProgressBarColours
        {
            public static System.Drawing.Color TASK_IN_PROGRESS_COLOUR = System.Drawing.Color.FromArgb(255, 128, 128, 255); // light blue
            public static System.Drawing.Color TASK_SUCCEEDED_COLOUR = System.Drawing.Color.FromArgb(255,42,255,42); // light green
            public static System.Drawing.Color TASK_FAILED_COLOUR = System.Drawing.Color.FromArgb(255, 255, 42, 42); // red
            public static System.Drawing.Color TASK_WARNING_COLOUR = System.Drawing.Color.FromArgb(255, 255, 212, 42); // yellow
            public static System.Drawing.Color IDLE_COLOUR = System.Drawing.Color.FromArgb(255, 192, 182, 182); // light gray
        }

        /// <summary>
        /// Once instance of the logger used by any class.
        /// </summary>
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