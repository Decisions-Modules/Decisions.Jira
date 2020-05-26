using System;
using System.IO;
using System.Net;
using System.Threading;
using Decisions.Jira;
using Decisions.Jira.Steps;
using DecisionsFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{

    [TestClass]
    public class IssueTests
    {
        private JiraCredentials CloudCredential { get { return TestData.GetJiraCredentials(); } }
        private JiraCredentials ServerCredential { get { return TestData.GetServerJiraCredentials(); } }

        private JiraUserModel newUser;
        private JiraCreateUserResult createUserResult;
        private JiraProjectModel project;
        private JiraCreateProjectResult createProjectResult;

        [TestInitialize]
        public void Initialize()
        {
            Log.LogToFile = false;
        }

        private void CreateEntities(JiraCredentials credential)
        {
            newUser = TestData.GetJiraUser();
            createUserResult = UserSteps.CreateUser(credential, newUser);
            project = TestData.GetJiraProject(createUserResult.Data);
            createProjectResult = ProjectSteps.CreateProject(credential, project);
        }

        private void DeleteEntities(JiraCredentials credential)
        {
            try
            {
                ProjectSteps.DeleteProject(credential, project.ProjectIdOrKey);
                UserSteps.DeleteUser(credential, createUserResult.Data.Key); // for Jira server
                UserSteps.DeleteUser(credential, createUserResult.Data.AccountId); // for Jira cloud
            }
            catch (Exception ex) { _ = ex.Message; }
        }


        [TestMethod]
        public void Create()
        {
            TestCreateIssue(CloudCredential);
            TestCreateIssue(ServerCredential);
        }

        private void TestCreateIssue(JiraCredentials credential)
        {
            CreateEntities(credential);

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateResult = IssueSteps.CreateIssue(credential, issue);
                Assert.AreEqual(CreateResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void Edit()
        {
            TestEditIssue(ServerCredential);
            TestEditIssue(CloudCredential);
        }

        private void TestEditIssue(JiraCredentials credential)
        {
            CreateEntities(credential);
            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);

                JiraCreateIssueResult createIssueResult = IssueSteps.CreateIssue(credential, issue);

                issue.IssueIdOrKey = createIssueResult.Data.Key;
                issue.Issuetype = null;
                issue.Summary = null;
                issue.Description = "new edited description";
                issue.Project = null;


                BaseJiraResult editResult = IssueSteps.EditIssue(credential, issue);
                Assert.AreEqual(editResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                    DeleteEntities(credential);
            }
        }

        [TestMethod]
        public void Delete()
        {
            TestDeleteIssue(CloudCredential);
            TestDeleteIssue(ServerCredential);
        }

        private void TestDeleteIssue(JiraCredentials credential)
        {
            CreateEntities(credential);

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateResult = IssueSteps.CreateIssue(credential, issue);

                JiraIssueDeleteModel deleteModel = new JiraIssueDeleteModel { IssueIdOrKey = CreateResult.Data.Id, DeleteSubtasks = true };
                var DeleteResult = IssueSteps.DeleteIssue(credential, deleteModel);

                Assert.AreEqual(DeleteResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(credential);
            }

        }

        [TestMethod]
        public void Assign()
        {
            TestAssignIssue(CloudCredential);
            TestAssignIssue(ServerCredential);
        }

        private void TestAssignIssue(JiraCredentials credential)
        {
            CreateEntities(credential);

            if(credential.JiraConnection==JiraConnectionType.JiraCloud)
                Thread.Sleep(10000); // Jira won't allow to AssignIssue without this pause (after User creating).

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateIssueResult = IssueSteps.CreateIssue(credential, issue);

                JiraAssigneeModel Assign = new JiraAssigneeModel
                {
                    AccountId = createUserResult.Data.AccountId,
                    Key = createUserResult.Data.Key,
                    IssueIdOrKey = CreateIssueResult.Data.Key
                };
                
                BaseJiraResult AssigneResult = IssueSteps.AssignIssue(credential, Assign);
                Assert.AreEqual(AssigneResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(credential);
            }

        }

    }
}
