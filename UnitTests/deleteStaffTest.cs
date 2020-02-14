using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class deleteStaffTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Staff staff = new Staff();
            staff.setID(3);

            // act
            StaffDAO dao = new StaffDAO();
            int result = dao.deleteStaff(staff);

            // assert
            Assert.AreEqual(1, result);
        }
    }
}
