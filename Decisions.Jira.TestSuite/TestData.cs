﻿
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
                ProjectIdOrKey = aProjectIdOrKey,
                Users = new string[] { aUser },
                RoleId = aRoleId

            };
        }


        public static JiraIssueModel GetJiraIssue(string projectId, string jiraIssueId)
        {
            return new JiraIssueModel
            {
                Summary = "New Issue " + DateTime.Now.ToString("ddmmyyhhss"),
                Project = new JiraIdReferenceModel(projectId),
                Issuetype = new JiraIdReferenceModel(jiraIssueId),
                Description = " this is issue description"
            };
        }


    }
}
