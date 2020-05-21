
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
		public static JiraCreateIssueResult CreateIssue(JiraCredentials Credentials, JiraIssueModel NewIssue)
		{
			try
			{
				var DataObj = new ActualJiraIssueModel { Fields = NewIssue };
				var response = JiraUtility.Post<ActualJiraIssueModel, JiraBaseResponseModel>("issue", Credentials, DataObj, HttpStatusCode.Created);
				return new JiraCreateIssueResult(response);
				/*
				string data = JsonConvert.SerializeObject(DataObj, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response = new JiraUtility().GetClient(Credentials).PostAsync("issue", content).Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.Created ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
				*/
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static BaseJiraResult DeleteIssue(JiraCredentials Credentials, JiraIssueDeleteModel IssueModel)
		{
			try
			{
				var response = JiraUtility.Delete($"issue/{IssueModel.IssueIdOrKey}?deleteSubtasks={IssueModel.DeleteSubtasks}", Credentials, HttpStatusCode.NoContent);
				return response;
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw;
			}
			/*try
			{
				var response = new JiraUtility().GetClient(Credentials).DeleteAsync($"issue/{IssueModel.IssueIdOrKey}?deleteSubtasks={IssueModel.DeleteSubtasks}").Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}*/
		}

		public static BaseJiraResult EditIssue(JiraCredentials Credentials, JiraIssueModel Issue)
		{
			try
			{
				var DataObj = new ActualJiraIssueModel { Fields = Issue };
				var response = JiraUtility.Put<ActualJiraIssueModel, JiraEmptyResponseModel>("issue/{Issue.IssueIdOrKey}", Credentials, DataObj, HttpStatusCode.Created);
				return new BaseJiraResult { ErrorMessage = response.ErrorMessage, Status = response.Status, HttpStatus = response.HttpStatus };
				/*				var DataObj = new { fields = Issue };
								string data = JsonConvert.SerializeObject(DataObj, Formatting.None,
											new JsonSerializerSettings
											{
												NullValueHandling = NullValueHandling.Ignore
											});
								var content =
								new StringContent(data, Encoding.UTF8, "application/json");
								var response = new JiraUtility().GetClient(Credentials).PutAsync($"issue/{Issue.IssueIdOrKey}", content).Result;
								var responseString = response.Content.ReadAsStringAsync().Result;
								return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent ? responseString : string.Empty };*/
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static BaseJiraResult AssignIssue(JiraCredentials Credentials, JiraAssigneeModel Assign)
		{
			try
			{
				var response = JiraUtility.Put<JiraAssigneeModel, JiraEmptyResponseModel>($"issue/{Assign.IssueIdOrKey}/assignee", Credentials, Assign, HttpStatusCode.NoContent);
				return new BaseJiraResult { ErrorMessage = response.ErrorMessage, Status = response.Status, HttpStatus = response.HttpStatus };
				/*				var DataObj = new { accountId = Assign.AccountId };
								string data = JsonConvert.SerializeObject(DataObj);
								var content =
								new StringContent(data, Encoding.UTF8, "application/json");
								var response = new JiraUtility().GetClient(Credentials).PutAsync($"issue/{Assign.IssueIdOrKey}/assignee", content).Result;
								var responseString = response.Content.ReadAsStringAsync().Result;
								return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent ? responseString : string.Empty };*/
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
