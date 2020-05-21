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

        public static string[] AvailableProjectTemplateKeys
        {
            get
            {
                return new string[] {"com.pyxis.greenhopper.jira:gh-simplified-agility-kanban",
                        "com.pyxis.greenhopper.jira:gh-simplified-agility-scrum",
                        "com.pyxis.greenhopper.jira:gh-simplified-basic",
                        "com.pyxis.greenhopper.jira:gh-simplified-kanban-classic",
                        "com.pyxis.greenhopper.jira:gh-simplified-scrum-classic",
                        "com.atlassian.servicedesk:simplified-it-service-desk",
                        "com.atlassian.servicedesk:simplified-internal-service-desk",
                        "com.atlassian.servicedesk:simplified-external-service-desk",
                        "com.atlassian.servicedesk:simplified-hr-service-desk",
                        "com.atlassian.servicedesk:simplified-facilities-service-desk",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-content-management",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-document-approval",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-lead-tracking",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-process-control",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-procurement",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-project-management",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-recruitment",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-task-tracking",
                        "com.atlassian.jira.jira-incident-management-plugin:im-incident-management",
                        "com.atlassian.jira-core-project-templates:jira-core-project-management" // for Jira Server
                };
            }
        }


        private HttpClient GetClient(JiraCredentials credentials)
        {
            if (credentials == null)
            {
                JiraSettings j = ModuleSettingsAccessor<JiraSettings>.GetSettings();
                credentials = new JiraCredentials();
                credentials.JiraURL = j.JiraURL;
                credentials.Password = j.Password;
                credentials.User = j.UserId;
                credentials.JiraConnection = j.JiraConnection;
            }

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(credentials.JiraURL.TrimEnd('/') + BASEPATH) };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationManager().GetAuthHeader(credentials);
            return httpClient;
        }

        private static JiraResultWithData ParseResponse<T>(HttpResponseMessage response, HttpStatusCode expectedStatus) where T : class, new()
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            var result = new JiraResultWithData()
            {
                Status = response.StatusCode == expectedStatus ? JiraResultStatus.Success : JiraResultStatus.Fail,
                HttpStatus = response.StatusCode,
                ErrorMessage = response.StatusCode != expectedStatus ? responseString : string.Empty,
                Data = response.StatusCode == expectedStatus ? JsonConvert.DeserializeObject<T>(responseString) : null
            };

            if (result.Status == JiraResultStatus.Fail && result.ErrorMessage.Length == 0)
            {
                result.ErrorMessage = $"{{ \"error\":{response.StatusCode} }}";
            }

            return result;
        }

        private static string ParseRequestContent<T>(T content)
        {
            string data = JsonConvert.SerializeObject(content, Formatting.None,
                           new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore
                           });
            return data;
        }

        public static JiraResultWithData Post<T, R>(string requestUri, JiraCredentials credentials, T content, HttpStatusCode expectedStatus) where R : class, new()
        {
            string data = ParseRequestContent(content);

            var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

            var client = new JiraUtility().GetClient(credentials);
            HttpResponseMessage response;

            response = client.PostAsync(requestUri, contentStr).Result;

            var result = ParseResponse<R>(response, expectedStatus);

            return result;
        }

        public static JiraResultWithData Put<T, R>(string requestUri, JiraCredentials credentials, T content, HttpStatusCode expectedStatus) where R : class, new()
        {
            string data = ParseRequestContent(content);

            var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

            var client = new JiraUtility().GetClient(credentials);
            HttpResponseMessage response;

            response = client.PutAsync(requestUri, contentStr).Result;

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

            return result;
        }

    }
}