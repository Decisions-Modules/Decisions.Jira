using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Create()
        {
            HttpStatusCode actualStatusCode = User.CreateUser(TestData.GetJiraCredentials(), TestData.GetJiraUser()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void AssignProject()
        {
            HttpStatusCode actualStatusCode = User.AssignProject(TestData.GetJiraCredentials(), TestData.GetJiraAssignProject()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}
