using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace Controller
{
    public class StaffController
    {
        public IStaffOps service { get; set; }

        // default constructor
        public StaffController()
        {
            service = POS.Configuration.staffOps;
        }

        // test constructor
        public StaffController(IStaffOps service)
        {
            this.service = service;
        }


        public void addStaff(IStaff newStaff)
        {
            service.addStaff(newStaff);
        }

        public void deleteStaff(int id)
        {
            service.delete(id);
        }
 
        // TODO: redundant methods - deal with it
        public void updateStaff(IStaff toUpdate)
        {
            service.updateStaff(toUpdate);
        }
        public void importUpdateStaff(IStaff toUpdate)
        {
            service.importUpdateStaff(toUpdate);
        }
    }
}
