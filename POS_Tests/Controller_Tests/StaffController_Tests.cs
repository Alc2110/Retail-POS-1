using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using Model.ObjectModel;
using Model.ServiceLayer;
using FakeItEasy;
using Controller;

namespace POS_Tests.Controller_Tests
{
    [TestClass]
    public class StaffController_Tests
    {
        // instance of the class under test
        protected StaffController staffController;

        [SetUp]
        public void setup()
        { }

        [TestMethod]
        public void deleteStaff_Test()
        {
            // arrange
            int id = 1;
            // set up the model fake
            var staffService = A.Fake<Model.ServiceLayer.IStaffOps>();
            bool deleteStaffMethodCalled = false;
            A.CallTo(() => staffService.delete(id)).Invokes(() => { deleteStaffMethodCalled = true; });
            // set up the class under test
            this.staffController = new StaffController(staffService);

            // act
            staffController.deleteStaff(id);

            // assert
            NUnit.Framework.Assert.IsTrue(deleteStaffMethodCalled);
        }

        [TestMethod]
        public void addStaff_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string password = "secret";
            Staff.Privelege privelege = Staff.Privelege.Normal;
            Staff newStaff = new Staff(id, fullName, password, privelege);
            // set up the model fake
            var staffService = A.Fake<Model.ServiceLayer.IStaffOps>();
            bool addStaffMethodCalled = false;
            A.CallTo(() => staffService.addStaff(newStaff)).Invokes(() => { addStaffMethodCalled = true; });
            // set up the class under test
            this.staffController = new StaffController(staffService);

            // act
            staffController.addStaff(newStaff);

            // assert
            NUnit.Framework.Assert.IsTrue(addStaffMethodCalled);
        }

        [TestMethod]
        public void updateStaff_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string password = "secret";
            Staff.Privelege privelege = Staff.Privelege.Normal;
            Staff newStaff = new Staff(id, fullName, password, privelege);
            // set up the model fake
            var staffService = A.Fake<Model.ServiceLayer.IStaffOps>();
            bool updateStaffMethodCalled = false;
            A.CallTo(() => staffService.updateStaff(newStaff)).Invokes(() => { updateStaffMethodCalled = true; });
            // set up the class under test
            this.staffController = new StaffController(staffService);

            // act
            staffController.updateStaff(newStaff);

            // assert
            NUnit.Framework.Assert.IsTrue(updateStaffMethodCalled);
        }

        [TestMethod]
        public void importUpdateStaff_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string password = "secret";
            Staff.Privelege privelege = Staff.Privelege.Normal;
            Staff newStaff = new Staff(id, fullName, password, privelege);
            // set up the model fake
            var staffService = A.Fake<Model.ServiceLayer.IStaffOps>();
            bool updateStaffMethodCalled = false;
            A.CallTo(() => staffService.importUpdateStaff(newStaff)).Invokes(() => { updateStaffMethodCalled = true; });
            // set up the class under test
            this.staffController = new StaffController(staffService);

            // act
            staffController.importUpdateStaff(newStaff);

            // assert
            NUnit.Framework.Assert.IsTrue(updateStaffMethodCalled);
        }
    }
}
