using System;
using System.Net;
using System.Threading;
using Decisions.Jira;
using Decisions.Jira.Steps;
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

        private void CreateEntities(JiraCredentials Credential)
        {
            newUser = TestData.GetJiraUser();
            createUserResult = UserSteps.CreateUser(Credential, newUser);
            project = TestData.GetJiraProject(createUserResult.Data);
            createProjectResult = ProjectSteps.CreateProject(Credential, project);
        }

        private void DeleteEntities(JiraCredentials Credential)
        {
            try
            {
                ProjectSteps.DeleteProject(Credential, project.ProjectIdOrKey);
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

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(Credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateResult = IssueSteps.CreateIssue(Credential, issue);
                Assert.AreEqual(CreateResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(Credential);
            }

        }

        [TestMethod]
        public void Edit()
        {
            DoEdit(ServerCredential);
            DoEdit(CloudCredential);
            
        }

        private void DoEdit(JiraCredentials Credential)
        {
            CreateEntities(Credential);
            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(Credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);

                JiraCreateIssueResult createIssueResult = IssueSteps.CreateIssue(Credential, issue);

                issue.IssueIdOrKey = createIssueResult.Data.Key;
                issue.Issuetype = null;
                issue.Details = null;
                issue.Description = "new edited description";
                issue.JiraProject = null;


                BaseJiraResult editResult = IssueSteps.EditIssue(Credential, issue);
                Assert.AreEqual(editResult.Status, JiraResultStatus.Success);
            }
            finally
            {
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

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(Credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateResult = IssueSteps.CreateIssue(Credential, issue);

                JiraIssueDeleteModel deleteModel = new JiraIssueDeleteModel { IssueIdOrKey = CreateResult.Data.Id, DeleteSubtasks = true };
                var DeleteResult = IssueSteps.DeleteIssue(Credential, deleteModel);

                Assert.AreEqual(DeleteResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(Credential);
            }

        }

        [TestMethod]
        public void Assign()
        {
            DoAssign(CloudCredential);
            DoAssign(ServerCredential);
        }

        private void DoAssign(JiraCredentials Credential)
        {
            CreateEntities(Credential);

            if(Credential.JiraConnection==JiraConnectionType.JiraCloud)
                Thread.Sleep(10000); // Jira won't allow to AssignIssue without this pause (after User creating).

            try
            {
                var result = ProjectSteps.GetProjectMetadateByKey(Credential, project.Key);
                JiraProjectMetadataModel projectMetadata = result.Data;

                var issue = TestData.GetJiraIssue(projectMetadata.Id, projectMetadata.Issuetypes[0].Id);
                JiraCreateIssueResult CreateIssueResult = IssueSteps.CreateIssue(Credential, issue);

                JiraAssigneeModel Assign = new JiraAssigneeModel
                {
                    AccountId = createUserResult.Data.AccountId,
                    Key = createUserResult.Data.Key,
                    IssueIdOrKey = CreateIssueResult.Data.Key
                };
                
                BaseJiraResult AssigneResult = IssueSteps.AssignIssue(Credential, Assign);
                Assert.AreEqual(AssigneResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                DeleteEntities(Credential);
            }

        }

    }
}
