using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void Create()
        {
            JiraProjectModel project = TestData.GetJiraProject();
            Project.DeleteProject(TestData.GetJiraCredentials(), project.ProjectIdOrKey);

            HttpStatusCode actualStatusCode = Project.CreateProject(TestData.GetJiraCredentials(), project).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
        [TestMethod]
        public void Edit()
        {
            JiraProjectModel project = TestData.GetJiraProject();
            Project.CreateProject(TestData.GetJiraCredentials(), project);

            project.Description = "Test Desc";
            project.Name = "Dec_wfh_1";

            HttpStatusCode actualStatusCode = Project.EditProject(TestData.GetJiraCredentials(), project).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void Delete()
        {
            JiraProjectModel project = TestData.GetJiraProject();
            Project.CreateProject(TestData.GetJiraCredentials(), project);
            HttpStatusCode actualStatusCode = Project.DeleteProject(TestData.GetJiraCredentials(), project.ProjectIdOrKey).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void GetProjectTypeByKey()
        {
            JiraProjectModel project = TestData.GetJiraProject();
            Project.CreateProject(TestData.GetJiraCredentials(), project);
            HttpStatusCode actualStatusCode = Project.GetProjectTypeByKey(TestData.GetJiraCredentials(), project.ProjectTypeKey).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void GetАccessibleProjectTypeByKey()
        {
            JiraProjectModel project = TestData.GetJiraProject();
            Project.CreateProject(TestData.GetJiraCredentials(), project);
            HttpStatusCode actualStatusCode = Project.GetАccessibleProjectTypeByKey(TestData.GetJiraCredentials(), project.ProjectTypeKey).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}
