
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

        private void CreateEntities(JiraCredentials Credential)
        {
            newUser = TestData.GetJiraUser();
            createUserResult = UserSteps.CreateUser(Credential, newUser);
        }

        private void DeleteEntities(JiraCredentials Credential)
        {
            try
            {
                UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira cloud
            }
            catch (Exception ex) { _ = ex.Message; }
        }


        [TestMethod]
        public void Create()
        {
            DoCreate(CloudCredential);
            DoCreate(ServerCredential);
        }

        private void DoCreate(JiraCredentials Credential)
        {
            CreateEntities(Credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                var response = ProjectSteps.CreateProject(Credential, project);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    DeleteEntities(Credential);
            }
        }

        [TestMethod]
        public void Edit()
        {
            DoEdit(CloudCredential);
            DoEdit(ServerCredential);
        }
        private void DoEdit(JiraCredentials Credential)
        {
            CreateEntities(Credential);

            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            try
            {
                var createResponse = ProjectSteps.CreateProject(Credential, project);

                project.Description = "Test Desc";
                project.Name = "Dec_wfh_1";

                var editResponse = ProjectSteps.EditProject(Credential, project);
                Assert.AreEqual(editResponse.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    DeleteEntities(Credential);
            }
        }

        [TestMethod]
        public void Delete()
        {
            DoDelete(CloudCredential);
            DoDelete(ServerCredential);
        }
        private void DoDelete(JiraCredentials Credential)
        {
            CreateEntities(Credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(Credential, project);
                var deleteResponse = ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                Assert.AreEqual(deleteResponse.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                DeleteEntities(Credential);
            }
        }

        [TestMethod]
        public void GetProjectTypeByKey() 
        {
            DoGetProjectTypeByKey(CloudCredential);
            DoGetProjectTypeByKey(ServerCredential);
        }
        private void DoGetProjectTypeByKey(JiraCredentials Credential)
        {
            CreateEntities(Credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetProjectTypeByKey(Credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                DeleteEntities(Credential);
            }
        }

        [TestMethod]
        public void GetАccessibleProjectTypeByKey() {
            DoGetАccessibleProjectTypeByKey(CloudCredential);
            DoGetАccessibleProjectTypeByKey(ServerCredential);
        }
        private void DoGetАccessibleProjectTypeByKey( JiraCredentials Credential)
        {
            CreateEntities(Credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetАccessibleProjectTypeByKey(Credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    DeleteEntities(Credential);
            }
        }

        [TestMethod]
        public void GetProjectRoles() 
        {
            DoGetProjectRoles(CloudCredential);
            DoGetProjectRoles(ServerCredential);
        }
        private void DoGetProjectRoles( JiraCredentials Credential)
        {
            CreateEntities(Credential);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetProjectRoles(Credential);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                DeleteEntities(Credential);
            }
        }
    }
}
