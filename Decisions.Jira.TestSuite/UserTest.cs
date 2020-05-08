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
             
            string userName = "test" + DateTime.Now.ToString("ddmmyyhhss");
            JiraUserModel jiraUserModel = new JiraUserModel
            {
                DisplayName = userName + " display",
                EmailAddress = userName + "@domain.com",
                Name = userName,
                Password = userName
            };
            HttpStatusCode actualStatusCode = User.Create(TestData.GetJiraCredentials(), jiraUserModel).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    
    [TestMethod]
    public void AssignProject()
    { 
         JiraAssignProjectModel jiraAssignModel = new JiraAssignProjectModel
         {
             ProjectIdOrKey = "10000",
             Users =new string[] { "5ea963ee9ce9ee0b8943fed2" },
             RoleId= 10006

         };
        HttpStatusCode actualStatusCode = User.AssignProject(TestData.GetJiraCredentials(), jiraAssignModel).Status;
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
        Assert.AreEqual(expectedStatusCode, actualStatusCode);
    }
}
}
