using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    public static class StaffOps
    {
        public static void addStaff(string FullName, string PasswordHash, string role)
        {
            // object
            Staff newStaff = new Staff();
            newStaff.setName(FullName);
            newStaff.setPasswordHash(PasswordHash);
            switch (role)
            {
                case "Admin":
                    newStaff.setPrivelege(Staff.Privelege.Admin);
                    break;
                case "Normal":
                    newStaff.setPrivelege(Staff.Privelege.Normal);
                    break;
                default:
                    // this shouldn't happen
                    throw new Exception("Invalid data input");
            }

            // DAO
            StaffDAO dao = new StaffDAO();
            dao.addStaff(newStaff);
        }
    }
}
