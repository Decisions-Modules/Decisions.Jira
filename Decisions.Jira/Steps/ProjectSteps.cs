
using DecisionsFramework.Design.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Steps
{
    [AutoRegisterMethodsOnClass(true, "Integration/Jira/Projects")]
    public static class ProjectSteps
    {
		public static JiraCreateProjectResult CreateProject(JiraCredentials credential, JiraProjectModel newProject)
		{
				var response = JiraUtility.Post<JiraProjectModel, JiraBaseResponseModel>("project", credential, newProject, HttpStatusCode.Created);
				return new JiraCreateProjectResult(response);
		}


		public static BaseJiraResult DeleteProject(JiraCredentials credential, string projectIdOrKey)
		{
				var response = JiraUtility.Delete($"project/{projectIdOrKey}", credential, HttpStatusCode.NoContent);
				return response;
		}

		public static JiraEditProjectResult EditProject(JiraCredentials credential, JiraProjectModel projectModel)
		{
				var response = JiraUtility.Put<JiraProjectModel, JiraEditProjectResponseModel>($"project/{projectModel.ProjectIdOrKey}", credential, projectModel, HttpStatusCode.OK);
				return new JiraEditProjectResult(response);
		}

		public static JiraProjectMetadataResult GetProjectMetadateByKey(JiraCredentials credential, string projectKey)
		{
				var response = JiraUtility.Get<JiraProjectMetadataResponseModel>($"issue/createmeta?projectKeys={projectKey}", credential);
				return new JiraProjectMetadataResult(response);
		}

		public static JiraProjectTypeResult GetProjectTypeByKey(JiraCredentials credential, string projectTypeKey)
		{
				var response = JiraUtility.Get<JiraProjectTypeResponseModel>($"project/type/{projectTypeKey}", credential);
				return new JiraProjectTypeResult(response);
		}

		public static JiraProjectTypeResult GetАccessibleProjectTypeByKey(JiraCredentials credential, string projectTypeKey)
		{
				var response = JiraUtility.Get<JiraProjectTypeResponseModel>($"project/type/{projectTypeKey}/accessible", credential);
				return new JiraProjectTypeResult(response);
		}

		public static JiraProjectRolesResult GetProjectRoles(JiraCredentials credential)
		{
				var response = JiraUtility.Get<List<JiraProjectRolesResponseModel>>($"/rest/api/2/role", credential);
				return new JiraProjectRolesResult(response);
		}

	}
}
