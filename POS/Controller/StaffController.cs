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
        // default ctor
        public StaffController()
        { }

        public void addStaff(string FullName, string PasswordHash, string role)
        {
            Model.ObjectModel.Staff toAdd = new Model.ObjectModel.Staff();
            toAdd.setName(FullName);
            toAdd.setPasswordHash(PasswordHash);
            switch (role)
            {
                case "Admin":
                    toAdd.setPrivelege(Model.ObjectModel.Staff.Privelege.Admin);
                    break;
                case "Normal":
                    toAdd.setPrivelege(Model.ObjectModel.Staff.Privelege.Normal);
                    break;
                default:
                    // shouldn't happen
                    return;
            }

            POS.Configuration.staffOps.addStaff(toAdd);
        }

        public void deleteStaff(int id)
        {
            POS.Configuration.staffOps.delete(id);
        }
        
        public void updateStaff(int id, string FullName, string PasswordHash, string role)
        {
            Model.ObjectModel.Staff toUpdate = new Model.ObjectModel.Staff();
            toUpdate.setID(id);
            toUpdate.setName(FullName);
            toUpdate.setPasswordHash(PasswordHash);
            switch (role)
            {
                case "Admin":
                    toUpdate.setPrivelege(Model.ObjectModel.Staff.Privelege.Admin);
                    break;
                case "Normal":
                    toUpdate.setPrivelege(Model.ObjectModel.Staff.Privelege.Normal);
                    break;
                default:
                    // shouldn't happen
                    return;
            }

            POS.Configuration.staffOps.updateStaff(toUpdate);
        }
    }
}
