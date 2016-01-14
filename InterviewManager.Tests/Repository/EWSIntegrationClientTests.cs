using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewManager.Models;

namespace InterviewManager.Tests.Repository
{
    /// <summary>
    /// Summary description for EWSIntegrationClientTests
    /// </summary>
    [TestClass]
    public class EWSIntegrationClientTests
    {
        public EWSIntegrationClientTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAvailability()
        {
            var rep = new EWSIntegrationClient();

            var request = new AvailabilityRequest
            {
                DurationMinutes = 60,
                NumberOfDaysFromNow = 30,
                Users = new List<string> { "mario@rossrmsdemo.onmicrosoft.com" }
            };

            var result = rep.GetAvailability(request).Result;

            Assert.IsNotNull(result.AvailabilityResult);
        }

        [TestMethod]
        public void SendEmail()
        {
            var rep = new EWSIntegrationClient();

            var request = new SendEmailRequest
            {
                Body = "Test Email from MVC App",
                Recipients = new List<string> { "mario@rossrmsdemo.onmicrosoft.com"  },
                Subject = "Test Email from MVC App Unit Test",
            };

            var result = rep.SendEmail(request).Result;

            Assert.IsNotNull(result.Recipients);
        }

        [TestMethod]
        public void CreateAppointment()
        {
            var rep = new EWSIntegrationClient();

            var start = new DateTime(2016, 1, 13, 13, 0, 0);
            var end = start.AddHours(1);

            var request = new CreateAppointmentRequest
            {
                Body = "Test Appointment From MVC Unit Test",
                Location = "MVC Unit Test",
                Subject = "MVC Unit Test Appointment",
                Start = start.ToString(),
                End = end.ToString()
            };

            var result = rep.CreateAppointment(request).Result;

            Assert.IsNotNull(result.AppointId);
        }
    }
}
