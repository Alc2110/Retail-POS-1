using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    public class StaffOps
    {
        public void addStaff(Staff newStaff)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.addStaff(newStaff);

            // fire the event
            getAllStaff();
        }

        public Staff getStaff(int id)
        {
            // DAO
            // retrieve from database
            StaffDAO dao = new StaffDAO();
            return dao.getStaff(id);
        }

        public void updateStaff(Staff staff)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.updateStaff(staff);

            // fire the event
            getAllStaff();
        }

        public void importUpdateStaff(Staff staff)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.importUpdateStaff(staff);
        }

        public void delete(int id)
        {
            // DAO
            StaffDAO dao = new StaffDAO();
            dao.deleteStaff(id);

            // fire the event
            getAllStaff();
        }

        public List<Staff> getAllStaff()
        {
            // DAO
            // retrieve from database
            StaffDAO dao = new StaffDAO();
            List<Staff> staffList = dao.getAllStaff();

            // fire the event
            GetAllStaff(this, new GetAllStaffEventArgs(staffList));

            return staffList;
        }

        // event for getting all staff
        public event EventHandler<GetAllStaffEventArgs> GetAllStaff;
        protected virtual void OnGetAllStaff(GetAllStaffEventArgs args)
        {
            GetAllStaff?.Invoke(this, args);
        }
    }

    /// <summary>
    /// Event arguments class
    /// </summary>
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
