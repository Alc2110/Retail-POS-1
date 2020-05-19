using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.DataAccessLayer.SqlServerInterface;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    public class StaffOps : IStaffOps
    {
        // data access dependency injection
        public IStaffDAO dataAccessObj { get; set; }
        // default constructor
        public StaffOps()
        {
            dataAccessObj = new StaffDAO();
        }
        // test constructor
        public StaffOps(IStaffDAO dataAccessObj)
        {
            this.dataAccessObj = dataAccessObj;
        }

        public void addStaff(IStaff newStaff)
        {
            dataAccessObj.addStaff(newStaff);

            // fire the event to update the view
            getAllStaff();
        }

        public IStaff getStaff(int id)
        {
            return dataAccessObj.getStaff(id);
        }

        public void updateStaff(IStaff staff)
        {
            dataAccessObj.updateStaff(staff);

            // fire the event to update the view
            getAllStaff();
        }

        public void importUpdateStaff(IStaff staff)
        {
            dataAccessObj.importUpdateStaff(staff);

            // fire the event to update the view
            getAllStaff();
        }

        public void delete(int id)
        {
            dataAccessObj.deleteStaff(id);

            // fire the event to update the view
            getAllStaff();
        }

        public IEnumerable<IStaff> getAllStaff()
        {
            IEnumerable<IStaff> staffList = dataAccessObj.getAllStaff();

            // fire the event
            GetAllStaff(this, new GetAllStaffEventArgs(staffList));

            return staffList;
        }

        // event for getting all staff
        public event EventHandler<GetAllStaffEventArgs> GetAllStaff;
        protected virtual void OnGetAllStaff(GetAllStaffEventArgs args)
        {
            EventHandler<GetAllStaffEventArgs> tmp = GetAllStaff;
            if (tmp != null)
            {

                GetAllStaff?.Invoke(this, args);
            }
        }
    }

    public interface IStaffOps
    {
        IStaff getStaff(int id);
        void addStaff(IStaff newStaff);
        void updateStaff(IStaff staff);
        void importUpdateStaff(IStaff staff);
        void delete(int id);
        IEnumerable<IStaff> getAllStaff();
    }

    /// <summary>
    /// Event arguments class
    /// </summary>
    public class GetAllStaffEventArgs : EventArgs
    {
        private IEnumerable<IStaff> staffList;

        // constructor
        public GetAllStaffEventArgs(IEnumerable<IStaff> staffList)
        {
            this.staffList = staffList;
        }

        public IEnumerable<IStaff> getList()
        {
            return staffList;
        }
    }
}
