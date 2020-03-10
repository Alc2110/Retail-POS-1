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

            // fire the event
            getAllStaff();
        }

        public static void addStaff(Staff staff)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.addStaff(staff);
        }

        public static Staff getStaff(int id)
        {
            // DAO
            // retrieve from database
            StaffDAO dao = new StaffDAO();
            return dao.getStaff(id);
        }

        public static void updateStaff(Staff staff)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.updateStaff(staff);
        }

        // event for getting all staff
        public static event EventHandler<GetAllStaffEventArgs> OnGetAllStaff = delegate { };

        public static List<Staff> getAllStaff()
        {
            // DAO
            // retrieve from database
            StaffDAO dao = new StaffDAO();
            List<Staff> staffList = (List<Staff>)dao.getAllStaff();

            // fire the event
            OnGetAllStaff(null, new GetAllStaffEventArgs(staffList));

            return staffList;
        }
    }

    public class GetAllStaffEventArgs : EventArgs
    {
        private List<Staff> staffList;

        // ctor
        public GetAllStaffEventArgs(List<Staff> staffList)
        {
            this.staffList = staffList;
        }

        public List<Staff> getList()
        {
            return staffList;
        }
    }
}
