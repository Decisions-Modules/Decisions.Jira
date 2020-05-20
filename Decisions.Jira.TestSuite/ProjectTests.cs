using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Decisions.Jira.Data.Project;
using Decisions.Jira.Data.User;
using Decisions.Jira.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class ProjectTests
    {

        JiraCredentials Credential { get{ return TestData.GetJiraCredentials(); } }

        [TestMethod]
        public void Create()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                var response = ProjectSteps.CreateProject(Credential, project);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void Edit()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

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
                try
                {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void Delete()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            ProjectSteps.CreateProject(Credential, project);
            var deleteResponse = ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
            Assert.AreEqual(deleteResponse.Status, JiraResultStatus.Success);

            UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
            UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
        }

        [TestMethod]
        public void GetProjectTypeByKey()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetProjectTypeByKey(Credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void GetАccessibleProjectTypeByKey()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);

            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetАccessibleProjectTypeByKey(Credential, project.ProjectTypeKey);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void GetProjectRoles()
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);
            JiraProjectModel project = TestData.GetJiraProject(createUserResult.Data);
            try
            {
                ProjectSteps.CreateProject(Credential, project);

                var response = ProjectSteps.GetProjectRoles(Credential);
                Assert.AreEqual(response.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }
    }
}
