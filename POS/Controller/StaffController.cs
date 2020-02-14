using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;

namespace Controller
{
    public class StaffController
    {
        private static StaffController instance;

        private StaffController()
        { }

        public static StaffController getInstance()
        {
            if (instance == null)
            {
                instance = new StaffController();
            }

            return instance;
        }

        public void addStaff(string FullName, string PasswordHash, string role)
        {
            StaffOps.addStaff(FullName, PasswordHash, role);
        }

        public void deleteStaff(int id)
        { }
        
        public void updateStaff()
        { }
    }
}
