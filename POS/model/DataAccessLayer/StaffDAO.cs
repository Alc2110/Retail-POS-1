using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace Model.DataAccessLayer
{
    public class StaffDAO : IStaffDAO
    {
        public IList<Staff> getAllStaff()
        {
            IList<Staff> staffList = new List<Staff>();

            return staffList;
        }

        public void deleteStaff(Staff staff)
        {

        }

        public void addStaff(Staff staff)
        {

        }

        public void updateStaff(Staff newStaff, Staff oldStaff)
        {

        }
    }
}
