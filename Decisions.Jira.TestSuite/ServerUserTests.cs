using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Decisions.JiraTestSuite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Decisions.Jira.serverApi;

namespace Decisions.Jira.TestSuite
{
    public static class JiraResultExtentions
    {
        public static string GetKey(this JiraResult result)
        {
            JObject jsonObj = JObject.Parse(result.Data.ToString());
            return jsonObj.SelectToken("key").ToString();
        }
    };

    [TestClass]
    public class ServerUserTests
    {
        JiraCredentials Creditinals { get { return TestData.GetServerJiraCredentials(); } }

        [TestMethod]
        public void Create()
        {
            var newUser = TestData.GetJiraUser();
            JiraResult createResult = ServerUser.CreateUser(Creditinals, newUser);
            HttpStatusCode actualStatusCode = createResult.Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);

            ServerUser.DeleteUser(Creditinals, newUser.Name);
        }

        [TestMethod]
        public void AssignProject()
        {
            HttpStatusCode actualStatusCode = ServerUser.AssignProject(Creditinals, TestData.GetJiraAssignProject()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void EditUser()
        {
            var editedUser = TestData.GetJiraUser();
            JiraUserModel newUserData = new JiraUserModel
            {
                DisplayName = "edited" + editedUser.DisplayName,
                EmailAddress = "edited" + editedUser.EmailAddress,
                Name = "edited" + editedUser.Name,
                Password = null
            };

            JiraResult createResult = ServerUser.CreateUser(Creditinals, editedUser);
            try
            {
                HttpStatusCode actualStatusCode = ServerUser.EditUser(Creditinals, editedUser.Name, newUserData).Status;
                HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
                Assert.AreEqual(expectedStatusCode, actualStatusCode);
            }
            finally
            {
                try
                {
                    ServerUser.DeleteUser(Creditinals, newUserData.Name); // delete new user name
                    ServerUser.DeleteUser(Creditinals, editedUser.Name);  // delete old user name in case it was not updated
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void SetUserPassword()
        {
            //var editedUser = TestData.GetJiraUser();
            var editedUser = new JiraUserModel
            {
                DisplayName = "display name",
                EmailAddress = "ivan_kov@inbox.ru",
                Name = "ivan_kov",
                Password = "password"
            };
            JiraResult createResult = User.CreateUser(Creditinals, editedUser);
            try
            {
                HttpStatusCode actualStatusCode = ServerUser.SetUserPassword(Creditinals, editedUser.Name, "123").Status;
                HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
                Assert.AreEqual(expectedStatusCode, actualStatusCode);
            }
            finally
            {
                try
                {
                    ServerUser.DeleteUser(Creditinals, editedUser.Name);
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            JiraResult createResult = ServerUser.CreateUser(Creditinals, TestData.GetJiraUser());

            HttpStatusCode actualStatusCode = ServerUser.DeleteUser(Creditinals, createResult.GetAccountId()).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}
