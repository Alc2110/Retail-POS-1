using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace Model.DataAccessLayer
{
    public interface IStaffDAO
    {
        IList<Staff> getAllStaff();
        int deleteStaff(Staff staff);
        int addStaff(Staff staff);
        int updateStaff(Staff oldStaff, Staff newStaff);
    }
}
