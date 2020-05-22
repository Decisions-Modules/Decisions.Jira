
using DecisionsFramework;
using DecisionsFramework.Design.Flow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Steps
{

	[AutoRegisterMethodsOnClass(true, "Integration/Jira/Issues")]
	public static class IssueSteps
	{
		internal class ActualJiraIssueModel {

			[JsonProperty(PropertyName = "fields")]
			public JiraIssueModel Fields { get; set; }
		}
		public static JiraCreateIssueResult CreateIssue(JiraCredentials credential, JiraIssueModel newIssue)
		{
			try
			{
				var DataObj = new ActualJiraIssueModel { Fields = newIssue };
				var response = JiraUtility.Post<ActualJiraIssueModel, JiraBaseResponseModel>("issue", credential, DataObj, HttpStatusCode.Created);
				return new JiraCreateIssueResult(response);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static BaseJiraResult DeleteIssue(JiraCredentials credential, JiraIssueDeleteModel IssueModel)
		{
			try
			{
				var response = JiraUtility.Delete($"issue/{IssueModel.IssueIdOrKey}?deleteSubtasks={IssueModel.DeleteSubtasks}", credential, HttpStatusCode.NoContent);
				return response;
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw;
			}

		}

		public static BaseJiraResult EditIssue(JiraCredentials credential, JiraIssueModel Issue)
		{
			try
			{
				var DataObj = new ActualJiraIssueModel { Fields = Issue };

				var response = JiraUtility.Put<ActualJiraIssueModel, JiraEmptyResponseModel>($"issue/{Issue.IssueIdOrKey}", credential, DataObj, HttpStatusCode.NoContent);

				return new BaseJiraResult { ErrorMessage = response.ErrorMessage, Status = response.Status, HttpStatus = response.HttpStatus };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static BaseJiraResult AssignIssue(JiraCredentials credential, JiraAssigneeModel Assign)
		{
			try
			{
				var response = JiraUtility.Put<JiraAssigneeModel, JiraEmptyResponseModel>($"issue/{Assign.IssueIdOrKey}/assignee", credential, Assign, HttpStatusCode.NoContent);
				return new BaseJiraResult { ErrorMessage = response.ErrorMessage, Status = response.Status, HttpStatus = response.HttpStatus };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
