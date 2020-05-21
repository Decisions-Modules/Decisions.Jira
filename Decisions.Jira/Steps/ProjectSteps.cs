using Decisions.Jira.Data;
using Decisions.Jira.Data.Project;
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
		public static JiraCreateProjectResult CreateProject(JiraCredentials Credentials, JiraProjectModel NewProject)
		{
			try
			{
				var response = JiraUtility.Post<JiraProjectModel, JiraBaseResponseModel>("project", Credentials, NewProject, HttpStatusCode.Created);
				return new JiraCreateProjectResult(response);
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw;
			}
		}


		public static BaseJiraResult DeleteProject(JiraCredentials Credentials, string ProjectIdOrKey)
		{
			try
			{
				var response = JiraUtility.Delete($"project/{ProjectIdOrKey}", Credentials, HttpStatusCode.NoContent);
				return response;
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw;
			}
		}

		public static JiraEditProjectResult EditProject(JiraCredentials Credentials, JiraProjectModel ProjectModel)
		{
			try
			{
				var response = JiraUtility.Put<JiraProjectModel, JiraEditProjectResponseModel>($"project/{ProjectModel.ProjectIdOrKey}", Credentials, ProjectModel, HttpStatusCode.OK);
				return new JiraEditProjectResult(response);
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw;
			}
		}

		public static JiraProjectMetadataResult GetProjectMetadateByKey(JiraCredentials Credentials, string ProjectKey)
		{
			try
			{
				var response = JiraUtility.Get<JiraProjectMetadataResponseModel>($"issue/createmeta?projectKeys={ProjectKey}", Credentials);
				return new JiraProjectMetadataResult(response);
			}
			catch (Exception ex)
			{
				throw ;
			}
		}

		public static JiraProjectTypeResult GetProjectTypeByKey(JiraCredentials Credentials, string projectTypeKey)
		{
			try
			{
				var response = JiraUtility.Get<JiraProjectTypeResponseModel>($"project/type/{projectTypeKey}", Credentials);
				return new JiraProjectTypeResult(response);
			}
			catch (Exception ex)
			{
				throw ;
			}
		}

		public static JiraProjectTypeResult GetАccessibleProjectTypeByKey(JiraCredentials Credentials, string projectTypeKey)
		{
			try
			{
				var response = JiraUtility.Get<JiraProjectTypeResponseModel>($"project/type/{projectTypeKey}/accessible", Credentials);
				return new JiraProjectTypeResult(response);
			}
			catch (Exception ex)
			{
				throw;
			}

		}

		public static JiraProjectRolesResult GetProjectRoles(JiraCredentials Credentials)
		{
			try
			{
				var response = JiraUtility.Get<List<JiraProjectRolesResponseModel>>($"/rest/api/2/role", Credentials);
				return new JiraProjectRolesResult(response);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}
