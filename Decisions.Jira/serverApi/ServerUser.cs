using Decisions.Jira.Data;
using DecisionsFramework.Design.Flow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Decisions.Jira.serverApi
{
    [AutoRegisterMethodsOnClass(true, "Integration/JiraServer/Users")]
    public class ServerUser
    {
		public static JiraResult CreateUser(JiraCredentials Credentials, JiraUserModel JiraUserModel)
		{
			return User.CreateUser(Credentials, JiraUserModel);
		}

		public static JiraResult AssignProject(JiraCredentials Credentials, JiraAssignProjectModel JiraAssignmentModel)
		{
			try
			{
				var DataObj = new { user = JiraAssignmentModel.Users };
				string data = JsonConvert.SerializeObject(DataObj);
				var content =
				new StringContent(data, Encoding.UTF8, "application/json");
				var response = new Utility().GetClient(Credentials).PostAsync($"project/{JiraAssignmentModel.ProjectIdOrKey}/role/{JiraAssignmentModel.RoleId}", content).Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.OK ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.OK ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static JiraResult EditUser(JiraCredentials Credentials, string UserName, JiraUserModel JiraUserModel)
		{
			string data = JsonConvert.SerializeObject(JiraUserModel, Formatting.None,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});
			var content = new StringContent(data, Encoding.UTF8, "application/json");
			var response = new Utility().GetClient(Credentials).PutAsync($"user?username={UserName}", content).Result;
			var responseString = response.Content.ReadAsStringAsync().Result;
			return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
		}

		public static JiraResult SetUserPassword(JiraCredentials Credentials, string UserName, string newPassword)
		{
			string data = "{ \"password\": \"" + newPassword + "\"}";
			var content = new StringContent(data, Encoding.UTF8, "application/json");
			var response = new Utility().GetClient(Credentials).PutAsync($"user/password?username={UserName}", content).Result;
			var responseString = response.Content.ReadAsStringAsync().Result;
			return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
		}


		public static JiraResult DeleteUser(JiraCredentials Credentials, string UserName)
		{
			try
			{
				var response = new Utility().GetClient(Credentials).DeleteAsync($"user?username={UserName}").Result;
				var responseString = response.Content.ReadAsStringAsync().Result;
				return new JiraResult { Message = response.StatusCode != HttpStatusCode.NoContent ? responseString : string.Empty, Status = response.StatusCode, Data = response.StatusCode == HttpStatusCode.Created ? responseString : string.Empty };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
