
using Decisions.Jira;
using Decisions.Jira.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.JiraTestSuite
{
    class TestData
    {
        public static JiraCredentials GetJiraCredentials()
        {
            return new JiraCredentials
            {
                User = "ivan@grafsoft.org",
                Password = "OCjzLDJFXlelROp1a1wj0CC0",
                JiraURL = "https://ivankovalchuk.atlassian.net",
                JiraConnection = JiraConnectionType.JiraCloud
            };
        }
        public static JiraCredentials GetServerJiraCredentials()
        {
            return new JiraCredentials
            {
                User = "ivan",
                Password = "password",
                JiraURL = "http://localhost:8080",
                JiraConnection = JiraConnectionType.JiraServer

            };
        }

        public static JiraProjectModel GetJiraProject(JiraCreateUserResponseModel createUserResponse)
        {
            string aProjectTemplateKey;
            if (createUserResponse.AccountId == null)
                aProjectTemplateKey = "com.atlassian.jira-core-project-templates:jira-core-project-management"; // for Jira Server
            else
                aProjectTemplateKey = "com.pyxis.greenhopper.jira:gh-simplified-agility-kanban"; // for Jira Cloud

            return new JiraProjectModel
            {
                Description = "New Project123",
                Name = "Test123",
                Key = "NP123",
                ProjectIdOrKey = "NP123",
                ProjectTemplateKey = aProjectTemplateKey, 
                LeadAccountId = createUserResponse.AccountId,
                Lead= createUserResponse.Name,
                AssigneeType = ProjectLead.UNASSIGNED,
                ProjectTypeKey = "business"

            };
        }

        public static JiraUserModel GetJiraUser()
        {
            string userName = "test" + DateTime.Now.ToString("ddmmyyhhss");
            return new JiraUserModel
            {
                DisplayName = userName + " display",
                EmailAddress = userName + "@domain.com",
                Name = userName,
                Password = userName
            };
        }

        public static JiraAssignProjectModel GetJiraAssignProject(string aProjectIdOrKey, string aUser, long aRoleId)
        {
            return new JiraAssignProjectModel
            {
                /*ProjectIdOrKey = "10000",
                Users = new string[] { "5ea963ee9ce9ee0b8943fed2" },
                RoleId = 10006*/

                /*ProjectIdOrKey = GetJiraProject().ProjectIdOrKey,
                Users = new string[] { "5eb56717a4c57d0b8b2575d9" },
                RoleId = 10029*/
                ProjectIdOrKey = aProjectIdOrKey,
                Users = new string[] { aUser },
                RoleId = aRoleId

            };
        }


        public static JiraIssueModel GetJiraIssue(string projectId, string jiraIssueId)
        {
            return new JiraIssueModel
            {
                Details = "New Issue " + DateTime.Now.ToString("ddmmyyhhss"),
                JiraProject = new JiraIdReferenceModel(projectId),
                Issuetype = new JiraIdReferenceModel(jiraIssueId),
                Description = " this is issue description"
            };
        }


    }
}
