using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Decisions.JiraTestSuite
{

    public static class JiraResultExtentions
    {
        public static string GetAccountId(this JiraResult result)
        {
            JObject jsonObj = JObject.Parse(result.Data.ToString());
            return jsonObj.SelectToken("accountId").ToString();
        }
    };

    [TestClass]
    public class UserTest
    {
        JiraCredentials Creditinals { get { return TestData.GetJiraCredentials(); } }

        [TestMethod]
        public void Create()
        {
            JiraResult createResult = User.CreateUser(Creditinals, TestData.GetJiraUser());
            HttpStatusCode actualStatusCode = createResult.Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
           
            User.DeleteUser(TestData.GetJiraCredentials(), createResult.GetAccountId());
        }

        [TestMethod]
        public void AssignProject()
        {
            HttpStatusCode actualStatusCode = User.AssignProject(Creditinals, TestData.GetJiraAssignProject()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void DeleteUser()
        {
            JiraResult createResult = User.CreateUser(TestData.GetJiraCredentials(), TestData.GetJiraUser());

            HttpStatusCode actualStatusCode = User.DeleteUser(Creditinals, createResult.GetAccountId()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }



    }



}
