using DecisionsFramework;
using DecisionsFramework.Design.Flow;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Decisions.Jira.Steps
{
    [AutoRegisterMethodsOnClass(true, "Integration/Jira/Users")]
    public static class UserSteps
    {
        public static JiraCreateUserResult CreateUser(JiraCredentials credential, JiraUserModel jiraUserModel)
        {
            var response = JiraUtility.Post<JiraUserModel, JiraCreateUserResponseModel>("user", credential, jiraUserModel, HttpStatusCode.Created);
            return new JiraCreateUserResult(response);
        }

        public static BaseJiraResult DeleteUser(JiraCredentials credential, string accountIdOrKey)
        {
            BaseJiraResult response;
            if (credential.JiraConnection == JiraConnectionType.JiraCloud)
                response = JiraUtility.Delete($"user?accountId={accountIdOrKey}", credential, HttpStatusCode.NoContent);
            else
            if (credential.JiraConnection == JiraConnectionType.JiraServer)
                response = JiraUtility.Delete($"user?key={accountIdOrKey}", credential, HttpStatusCode.NoContent);
            else
                throw new NotSupportedException();

            return response;
        }

        public static JiraAssignProjectResult AssignProject(JiraCredentials credential, JiraAssignProjectModel jiraAssignmentModel)
        {
            var response = JiraUtility.Post<JiraAssignProjectModel, JiraProjectRolesResponseModel>($"project/{jiraAssignmentModel.ProjectIdOrKey}/role/{jiraAssignmentModel.RoleId}",
                            credential, jiraAssignmentModel, HttpStatusCode.OK);
            return new JiraAssignProjectResult(response);
        }

        public static JiraCreateUserResult EditUser(JiraCredentials credential, string key, JiraUserModel jiraUserModel)
        {
            if (credential.JiraConnection != JiraConnectionType.JiraServer)
                throw new BusinessRuleException("EditUser step can be used only for JiraConnectionType.JiraServer connection");

            var response = JiraUtility.Put<JiraUserModel, JiraCreateUserResponseModel>($"user?key={key}", credential, jiraUserModel, HttpStatusCode.OK);
            return new JiraCreateUserResult(response);
        }

        internal class UserPasswordModel
        {
            [JsonProperty(PropertyName = "password")]
            public string Password { get; set; }
        }
        public static BaseJiraResult SetUserPassword(JiraCredentials credential, string key, string newPassword)
        {
            if (credential.JiraConnection != JiraConnectionType.JiraServer)
                throw new BusinessRuleException("SetUserPassword step can be used only for JiraConnectionType.JiraServer connection");

            UserPasswordModel password = new UserPasswordModel { Password = newPassword };
            var response = JiraUtility.Put<UserPasswordModel, JiraEmptyResponseModel>($"user/password?key={key}", credential, password, HttpStatusCode.NoContent);

            return new BaseJiraResult { ErrorMessage = response.ErrorMessage, Status = response.Status, HttpStatus = response.HttpStatus };
        }
    }
}
