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
        string connString { get; set; }

        IEnumerable<IStaff> getAllStaff();
        IStaff getStaff(int id);
        void deleteStaff(int id);
        void addStaff(IStaff staff);
        void updateStaff(IStaff staff);
        void importUpdateStaff(IStaff staff);
    }
}
