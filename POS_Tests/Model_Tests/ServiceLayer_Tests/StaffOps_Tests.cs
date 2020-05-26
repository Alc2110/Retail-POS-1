using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using Model.ObjectModel;
using Model.ServiceLayer;
using FakeItEasy;

namespace POS_Tests.Model_Tests.ServiceLayer_Tests
{
    [TestClass]
    public class StaffOps_Tests
    {
        // an instance of the class under test
        StaffOps staffService;

        [SetUp]
        public void setup()
        {
            
        }

        [TestMethod]
        public void getStaff_Test()
        {
            // this should return either a null or a staff object

            // arrange
            // set up the data access fake
            var staffDAO = A.Fake<Model.DataAccessLayer.IStaffDAO>();
            int staffID = 1;
            string fullName = "Full Name";
            string password = "secret";
            Staff.Privelege privelege = Staff.Privelege.Normal;
            A.CallTo(() => staffDAO.getStaff(0)).Returns(null);
            A.CallTo(() => staffDAO.getStaff(staffID)).Returns(new Staff(staffID, fullName, password, privelege));
            // instantiate the class under test
            staffService = new StaffOps(staffDAO);

            // act
            IStaff staffThatDoesNotExist = this.staffService.getStaff(0);
            IStaff staffThatExists = this.staffService.getStaff(staffID);

            // assert
            NUnit.Framework.Assert.IsNull(staffThatDoesNotExist);
            NUnit.Framework.Assert.AreEqual(staffID, staffThatExists.StaffID);
            NUnit.Framework.Assert.AreEqual(fullName, staffThatExists.FullName);
            NUnit.Framework.Assert.AreEqual(password, staffThatExists.PasswordHash);
            NUnit.Framework.Assert.AreEqual(privelege, staffThatExists.privelege);
        }
    }
}
