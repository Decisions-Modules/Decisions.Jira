using Decisions.Jira.Data;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using DecisionsFramework.Design.Flow;

namespace Decisions.Jira
{
	[AutoRegisterMethodsOnClass(true, "Integration/Jira/Issues")]
	public static class IssueSteps
    {
        public static JiraResult CreateIssue (JiraCredentials Credentials, JiraIssue NewIssue)
        {
			try
			{ 
				var DataObj = new { fields = NewIssue };
				string data = JsonConvert.SerializeObject(DataObj, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							}); 
				var content =
				new StringContent(data, Encoding.UTF8, "application/json"); 
				var response = new JiraUtility().GetClient(Credentials).PostAsync("issue", content).Result;
				var responseString = response.Content.ReadAsStringAsync ().Result;
				return new JiraResult { Message = response.StatusCode!=HttpStatusCode.Created? responseString:string.Empty, Status = response.StatusCode,Data = response.StatusCode==HttpStatusCode.Created?responseString:string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static JiraResult DeleteIssue (JiraCredentials Credentials, JiraIssueDeleteModel IssueModel)
		{
			try
			{
				 var response =  new JiraUtility().GetClient(Credentials).DeleteAsync($"issue/{IssueModel.IssueIdOrKey}?deleteSubtasks={IssueModel.DeleteSubtasks}").Result;
				var responseString = response.Content.ReadAsStringAsync ().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		 
		public static JiraResult EditIssue(JiraCredentials Credentials, JiraIssue Issue)
		{
			try
			{
				var DataObj = new { fields = Issue };
				string data = JsonConvert.SerializeObject(DataObj, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response =   new JiraUtility().GetClient(Credentials).PutAsync ($"issue/{Issue.IssueIdOrKey}", content).Result;
				var responseString =   response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		public static JiraResult AssignIssue(JiraCredentials Credentials, JiraAssignee Assign)
		{
			try
			{
				var DataObj = new { accountId = Assign.AccountId };
				string data = JsonConvert.SerializeObject(DataObj);
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response =   new JiraUtility().GetClient(Credentials).PutAsync($"issue/{Assign.IssueIdOrKey}/assignee", content).Result;
				var responseString =   response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.NoContent? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
    }

