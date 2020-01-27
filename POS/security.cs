using System;
using System.Text;
using System.Security.Cryptography;

namespace Security
{
    public class Hasher
    {
        string fullName;
        string password;

        public Hasher(string fullName, string password)
        {
            this.fullName = fullName;
            this.password = password;
        }

        public string computeHash()
        {
            // create a SHA256
            using (SHA256 hash = SHA256.Create())
            {
                
                // include fullName as salt
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(fullName + password));

                // convert byte array to string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }

                // return the salted hash
                return builder.ToString(); 
            }
        }
    }
}