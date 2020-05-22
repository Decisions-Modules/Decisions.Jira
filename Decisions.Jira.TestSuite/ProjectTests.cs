
using Decisions.Jira;
using Decisions.Jira.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class ProjectTests
    {

        JiraCredentials CloudCredential { get { return TestData.GetJiraCredentials(); } }
        JiraCredentials ServerCredential { get { return TestData.GetServerJiraCredentials(); } }

        private JiraUserModel newUser;
        private JiraCreateUserResult createUserResult;

        private void CreateEntities(JiraCredentials credential)
        {
            newUser = TestData.GetJiraUser();
            createUserResult = UserSteps.CreateUser(credential, newUser);
        }

        private void DeleteEntities(JiraCredentials credential)
        {
            try
            {
                UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
                UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira cloud
            }
            catch (Exception ex) { _ = ex.Message; }
        }


        [TestMethod]
        public void Create()
        {
            TestCreateProject(CloudCredential);
            TestCreateProject(ServerCredential);
        }

        private void TestCreateProject(JiraCredentials credential)
        {
            CreateEntities(credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                var response = ProjectSteps.CreateProject(credential, project);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                    DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void Edit()
        {
            TestEditProject(CloudCredential);
            TestEditProject(ServerCredential);
        }
        private void TestEditProject(JiraCredentials credential)
        {
            CreateEntities(credential);

            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            try
            {
                var createResponse = ProjectSteps.CreateProject(credential, project);

                project.Description = "Test Desc";
                project.Name = "Dec_wfh_1";

                var editResponse = ProjectSteps.EditProject(credential, project);
                Assert.AreEqual(editResponse.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                    DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void Delete()
        {
            TestDeleteProject(CloudCredential);
            TestDeleteProject(ServerCredential);
        }
        private void TestDeleteProject(JiraCredentials credential)
        {
            CreateEntities(credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(credential, project);
                var deleteResponse = ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                Assert.AreEqual(deleteResponse.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void GetProjectTypeByKey() 
        {
            TestGetProjectTypeByKey(CloudCredential);
            TestGetProjectTypeByKey(ServerCredential);
        }
        private void TestGetProjectTypeByKey(JiraCredentials credential)
        {
            CreateEntities(credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(credential, project);

                var response = ProjectSteps.GetProjectTypeByKey(credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void GetАccessibleProjectTypeByKey() {
            TestGetАccessibleProjectTypeByKey(CloudCredential);
            TestGetАccessibleProjectTypeByKey(ServerCredential);
        }
        private void TestGetАccessibleProjectTypeByKey( JiraCredentials credential)
        {
            CreateEntities(credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            try
            {
                ProjectSteps.CreateProject(credential, project);

                var response = ProjectSteps.GetАccessibleProjectTypeByKey(credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                    DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void GetProjectRoles() 
        {
            TestGetProjectRoles(CloudCredential);
            TestGetProjectRoles(ServerCredential);
        }
        private void TestGetProjectRoles( JiraCredentials credential)
        {
            CreateEntities(credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(credential, project);

                var response = ProjectSteps.GetProjectRoles(credential);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                DeleteEntities(credential);
            }
        }
    }
}
