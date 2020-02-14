using System;

namespace Model.ObjectModel
{
    public class Staff : IActor
    {
        public enum Privelege
        {
            Normal,
            Admin
        }

        // default ctor
        public Staff()
        {
        }

        // ctor
        public Staff(int StaffID, string FullName, string PasswordHash, Privelege privelege)
        {
            this.StaffID = StaffID;
            this.FullName = FullName;
            this.PasswordHash = PasswordHash;
            this.privelege = privelege;
        }

        private int StaffID;
        private string FullName;
        private string PasswordHash;
        private Privelege privelege;

        public string getName()
        {
            return FullName;
        }

        public void setName(string name)
        {
            this.FullName = name;
        }

        public int getID()
        {
            return StaffID;
        }

        public void setID(int id)
        {
            this.StaffID = id;
        }

        public Privelege getPrivelege()
        {
            return privelege;
        }

        public void setPrivelege(Privelege privelege)
        {
            this.privelege = privelege;
        }

        public string getPasswordHash()
        {
            return PasswordHash;
        }

        public void setPasswordHash(string PasswordHash)
        {
            this.PasswordHash = PasswordHash;
        }
    }
}