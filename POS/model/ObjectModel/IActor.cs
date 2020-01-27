using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModel
{
    public interface IActor
    {
        void setID(int id);
        int getID();

        void setName(string name);
        string getName();
    }
}
