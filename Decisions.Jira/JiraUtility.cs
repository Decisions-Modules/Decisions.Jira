using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Decisions.Jira.Data;
using DecisionsFramework.ServiceLayer;
using Newtonsoft.Json;

namespace Decisions.Jira
{
    public class JiraUtility
    {
        private const string BASEPATH = "/rest/api/2/";

        public HttpClient GetClient(JiraCredentials credentials)
        {
            if (credentials == null)
            {
                JiraSettings j = ModuleSettingsAccessor<JiraSettings>.GetSettings();
                credentials = new JiraCredentials();
                credentials.JiraURL = j.JiraURL;
                credentials.Password = j.Password;
                credentials.User = j.UserId;
            }

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(credentials.JiraURL.TrimEnd('/') + BASEPATH) };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationManager().GetAuthHeader(credentials);
            return httpClient;
        }

        protected static JiraResultWithData ParseResponse<T>(HttpResponseMessage response, HttpStatusCode expectedStatus) where T : class, new()
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            var result = new JiraResultWithData()
            {
                Status = response.StatusCode == expectedStatus ? JiraResultStatus.Success : JiraResultStatus.Fail,
                ErrorMessage = response.StatusCode != expectedStatus ? responseString : string.Empty,
                Data = response.StatusCode == expectedStatus ? JsonConvert.DeserializeObject<T>(responseString) : null
            };

            return result;
        }

        public enum JiraHttpMethod { Post, Put }
        public static JiraResultWithData Request<T, R>(JiraHttpMethod method, string requestUri, JiraCredentials credentials, T content, HttpStatusCode expectedStatus) where R : class, new()
        {
            string data = JsonConvert.SerializeObject(content, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

            var client = new JiraUtility().GetClient(credentials);
            HttpResponseMessage response;

            switch (method)
            {
                case JiraHttpMethod.Post:
                    response = client.PostAsync(requestUri, contentStr).Result;
                    break;
                case JiraHttpMethod.Put:
                    response = client.PutAsync(requestUri, contentStr).Result;
                    break;
                default: throw new Exception("Incorrect method parameter value. Only Put or Post required");
            };

            var result = ParseResponse<R>(response, expectedStatus);

            return result;
        }

        public static BaseJiraResult Delete(string requestUri, JiraCredentials credentials, HttpStatusCode expectedStatus)
        {
            HttpResponseMessage response = new JiraUtility().GetClient(credentials).DeleteAsync(requestUri).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            var result = new BaseJiraResult()
            {
                Status = response.StatusCode == expectedStatus ? JiraResultStatus.Success : JiraResultStatus.Fail,
                ErrorMessage = response.StatusCode != expectedStatus ? responseString : string.Empty,
            };

            return result;
        }

        public static JiraResultWithData Get<R>(string requestUri, JiraCredentials credentials, HttpStatusCode expectedStatus = HttpStatusCode.OK) where R : class, new()
        {
            var client = new JiraUtility().GetClient(credentials);
            HttpResponseMessage response = new JiraUtility().GetClient(credentials).GetAsync(requestUri).Result; ;

            var responseString = response.Content.ReadAsStringAsync().Result;

            var result = ParseResponse<R>(response, expectedStatus);
            /*new GeneralJiraResult()
            {
                HttpStatus = response.StatusCode,
                Status = response.StatusCode == expectedStatus ? JiraRequestStatus.Success : JiraRequestStatus.Fail,
                ErrorMessage = response.StatusCode != expectedStatus ? responseString : string.Empty,
                Data = response.StatusCode == expectedStatus ? JsonConvert.DeserializeObject<R>(responseString) : null
            };*/

            return result;
        }

    }
}