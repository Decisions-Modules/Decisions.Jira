using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Steps;
using DecisionsFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Decisions.JiraTestSuite
{

    [TestClass]
    public class UserTest
    {
        JiraCredentials CloudCredential { get { return TestData.GetJiraCredentials(); } }
        JiraCredentials ServerCredential { get { return TestData.GetServerJiraCredentials(); } }

        [TestMethod]
        public void CreateUser()
        {
            TestCreateUser(CloudCredential);
            TestCreateUser(ServerCredential);
        }

        [TestInitialize]
        public void Initialize()
        {
            Log.LogToFile = false;
        }

        private void TestCreateUser(JiraCredentials credential)
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(credential, newUser);
            Assert.AreEqual(createUserResult.Status, JiraResultStatus.Success);

            UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira Cloud
            UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
        }

        [TestMethod]
        public void AssignProject()
        {
            TestAssignProject(CloudCredential);
            TestAssignProject(ServerCredential);
        }

        private void TestAssignProject(JiraCredentials credential)
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(credential, newUser);

            var newProject = TestData.GetJiraProject(createUserResult.Data);
            ProjectSteps.CreateProject(credential, newProject);
            JiraProjectRolesResult roles = ProjectSteps.GetProjectRoles(credential);

            try
            {
                JiraAssignProjectModel assignProject;
                if (credential.JiraConnection == JiraConnectionType.JiraCloud)
                    assignProject = TestData.GetJiraAssignProject(newProject.Key, createUserResult.Data.AccountId, roles.Data[0].Id);
                else
                    assignProject = TestData.GetJiraAssignProject(newProject.Key, createUserResult.Data.Key, roles.Data[0].Id);

                JiraAssignProjectResult assignProjectResult = UserSteps.AssignProject(credential, assignProject);
                Assert.AreEqual(assignProjectResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(credential, newProject.ProjectIdOrKey);
                    UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch { }
            }

        }

        [TestMethod]
        public void DeleteUser()
        {
            TestDeleteUser(CloudCredential);
            TestDeleteUser(ServerCredential);
        }

        private void TestDeleteUser(JiraCredentials credential)
        {

            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(credential, newUser);

            BaseJiraResult deleteResult;
            if (credential.JiraConnection == JiraConnectionType.JiraCloud)
                deleteResult=UserSteps.DeleteUser(credential, createUserResult.Data.AccountId);
            else
                deleteResult=UserSteps.DeleteUser(credential, createUserResult.Data.Key);

            Assert.AreEqual(deleteResult.Status, JiraResultStatus.Success);

        }

        [TestMethod]
        public void EditUser()
        {
            //doEditUser(CloudCredential);
            TestEditUser(ServerCredential);
        }

        private void TestEditUser(JiraCredentials credential)
        {
            var editedUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(credential, editedUser);

            JiraUserModel newUserData = new JiraUserModel
            {
                DisplayName = "edited" + editedUser.DisplayName,
                EmailAddress = "edited" + editedUser.EmailAddress,
                Name = "edited" + editedUser.Name,
                Password = null
            };

            try
            {
                var EditUserResult = UserSteps.EditUser(credential, createUserResult.Data.Key, newUserData);
                Assert.AreEqual(EditUserResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch { }
            }
        }

        [TestMethod]
        public void SetUserPassword()
        {
            //doSetUserPassword(CloudCredential);
            TestSetUserPassword(ServerCredential);
        }
        private void TestSetUserPassword(JiraCredentials credential)
        {
            var editedUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(credential, editedUser);

            try
            {
                var serPasswordResult = UserSteps.SetUserPassword(credential, createUserResult.Data.Key, "123");
                Assert.AreEqual(serPasswordResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch { }
            }
        }


    }



}
