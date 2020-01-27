using System;

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

        public class Logger
        {

        }
    }
}