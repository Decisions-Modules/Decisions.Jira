using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class IssueTest
    {
        [TestMethod]
        public void Create()
        { 
            JiraIssue jiraIssue = new JiraIssue
            {
                Details = "New Issue " + DateTime.Now.ToString("ddmmyyhhss"),
                JiraProject = new JiraProjectReferenceModel("10000"),
                Issuetype = new JiraIssueTypeModel("10001"),
               Description= " this is issue description"
            };
            HttpStatusCode actualStatusCode = IssueSteps.Create(TestData.GetJiraCredentials(), jiraIssue).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void Edit()
        { 
            JiraIssue jiraIssue = new JiraIssue
            {
                IssueIdOrKey = "DW-3",
                Details = "New Issue " + DateTime.Now.ToString("ddmmyyhhss"),
                JiraProject = new JiraProjectReferenceModel("10000"),
                Issuetype = new JiraIssueTypeModel("10001"),
                Description = " this is issue description updated"
            };
            HttpStatusCode actualStatusCode = IssueSteps.Edit(TestData.GetJiraCredentials(), jiraIssue).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void Delete()
        { 
            JiraIssueDeleteModel jiraIssueModel = new JiraIssueDeleteModel
            {
              DeleteSubtasks=true,
              IssueIdOrKey= "DW-7"
            };
            HttpStatusCode actualStatusCode = IssueSteps.Delete(TestData.GetJiraCredentials(), jiraIssueModel).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
        [TestMethod]
        public void Assign()
        { 
            JiraAssignee assign = new JiraAssignee
            {
                AccountId= "5ea963ee9ce9ee0b8943fed2",
                IssueIdOrKey ="DW-6"
            };
            HttpStatusCode actualStatusCode = IssueSteps.Assign(TestData.GetJiraCredentials(), assign).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

    }
}
