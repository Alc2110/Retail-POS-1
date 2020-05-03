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
        List<Staff> getAllStaff();
        void deleteStaff(int id);
        void addStaff(Staff staff);
        void updateStaff(Staff staff);
    }
}
