using Decisions.Jira.Data;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using DecisionsFramework.Design.Flow;

namespace Decisions.Jira
{
	[AutoRegisterMethodsOnClass(true, "Integration/Jira/Projects")]
    public static class Project
    {
         public static JiraResult Create(JiraCredentials Credentials, JiraProjectModel NewProject)
		{
			try
			{
				string data = JsonConvert.SerializeObject(NewProject, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response = new Utility().GetClient(Credentials).PostAsync("project", content).Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.Created ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static JiraResult Edit(JiraCredentials Credentials, JiraProjectModel ProjectModel)
		{
			try
			{
				string data = JsonConvert.SerializeObject(ProjectModel, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response = new Utility().GetClient(Credentials).PutAsync($"project/{ProjectModel.ProjectIdOrKey}", content).Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.OK ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.OK ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
