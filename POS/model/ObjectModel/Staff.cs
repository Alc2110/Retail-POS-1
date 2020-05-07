using System;

namespace Model.ObjectModel
{
    public class Staff : IStaff
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

        // constructor with parameters
        public Staff(int StaffID, string FullName, string PasswordHash, Privelege privelege)
        {
            this.StaffID = StaffID;
            this.FullName = FullName;
            this.PasswordHash = PasswordHash;
            this.privelege = privelege;
        }

        public int StaffID { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public Privelege privelege { get; set; }
    }

    public interface IStaff
    {
        int StaffID { get; set; }
        string FullName { get; set; }
        string PasswordHash { get; set; }
        Staff.Privelege privelege { get; set; }
    }
}