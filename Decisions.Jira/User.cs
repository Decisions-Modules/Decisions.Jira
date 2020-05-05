using Decisions.Jira.Data;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using DecisionsFramework.Design.Flow;

namespace Decisions.Jira
{
	[AutoRegisterMethodsOnClass(true, "Integration/Jira/Users")]
    public static class User
    {
		public static JiraResult Create(JiraCredentials Credentials, JiraUserModel JiraUserModel)
		{
			try
			{ 
				string data = JsonConvert.SerializeObject(JiraUserModel, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response =   new Utility().GetClient(Credentials).PostAsync("user", content).Result;
				var responseString =   response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.Created ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static JiraResult AssignProject(JiraCredentials Credentials, JiraAssignProjectModel JiraAssignmentModel)
		{
			try
			{
				var DataObj = new { user = JiraAssignmentModel.Users };
				string data = JsonConvert.SerializeObject(DataObj);
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response =   new Utility().GetClient(Credentials).PostAsync($"project/{JiraAssignmentModel.ProjectIdOrKey}/role/{JiraAssignmentModel.RoleId}", content).Result;
				var responseString =   response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.OK ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.OK ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		 
	}
}
