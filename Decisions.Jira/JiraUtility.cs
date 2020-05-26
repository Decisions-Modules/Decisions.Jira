using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DecisionsFramework;
using DecisionsFramework.ServiceLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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


        private static HttpClient GetClient(JiraCredentials credentials)
        {
            try
            {
                if (credentials == null)
                {
                    JiraSettings j = ModuleSettingsAccessor<JiraSettings>.GetSettings();
                    credentials = new JiraCredentials
                    {
                        JiraURL = j.JiraURL,
                        Password = j.Password,
                        User = j.UserId,
                        JiraConnection = j.JiraConnection
                    };
                }

                HttpClient httpClient = new HttpClient { BaseAddress = new Uri(credentials.JiraURL.TrimEnd('/') + BASEPATH) };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationManager().GetAuthHeader(credentials);
                return httpClient;
            }
            catch (Exception ex)
            {
                throw new LoggedException($"Error setting up connection to {credentials.JiraURL}", ex);
            }
        }

        private static JiraResultWithData ParseResponse<T>(HttpResponseMessage response, HttpStatusCode expectedStatus) where T : class, new()
        {
            try
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
            catch (Exception ex)
            {
                throw new LoggedException("Error parsing result", ex);
            }
        }

        private static string ParseRequestContent<T>(T content)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                };
                string data = JsonConvert.SerializeObject(content, Formatting.None, jsonSettings);
                return data;
            }
            catch (Exception ex)
            {
                throw new LoggedException("Error parsing request content", ex);
            }
        }

        public static JiraResultWithData Post<T, R>(string requestUri, JiraCredentials credentials, T content, HttpStatusCode expectedStatus) where R : class, new()
        {
            try
            {
                string data = ParseRequestContent(content);

                var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

                var client = JiraUtility.GetClient(credentials);

                HttpResponseMessage response = client.PostAsync(requestUri, contentStr).Result;

                return ParseResponse<R>(response, expectedStatus);
            }
            catch (Exception ex)
            {
                throw new LoggedException("Error to process POST to Jira", ex);
            }
        }

        public static JiraResultWithData Put<T, R>(string requestUri, JiraCredentials credentials, T content, HttpStatusCode expectedStatus) where R : class, new()
        {
            try
            {
                string data = ParseRequestContent(content);

                var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

                var client = JiraUtility.GetClient(credentials);

                HttpResponseMessage response = client.PutAsync(requestUri, contentStr).Result;

                return ParseResponse<R>(response, expectedStatus);
            }
            catch (Exception ex)
            {
                throw new LoggedException("Error to process PUT to Jira", ex);
            }
        }

        public static BaseJiraResult Delete(string requestUri, JiraCredentials credentials, HttpStatusCode expectedStatus)
        {
            try
            {
                HttpResponseMessage response = JiraUtility.GetClient(credentials).DeleteAsync(requestUri).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                return new BaseJiraResult()
                {
                    Status = response.StatusCode == expectedStatus ? JiraResultStatus.Success : JiraResultStatus.Fail,
                    HttpStatus = response.StatusCode,
                    ErrorMessage = response.StatusCode != expectedStatus ? responseString : string.Empty,
                };
            }
            catch (Exception ex)
            {
                throw new LoggedException("Error to process DELETE to Jira", ex);
            }
        }

        public static JiraResultWithData Get<R>(string requestUri, JiraCredentials credentials, HttpStatusCode expectedStatus = HttpStatusCode.OK) where R : class, new()
        {
            JiraResultWithData result;
            try
            {
                HttpResponseMessage response = JiraUtility.GetClient(credentials).GetAsync(requestUri).Result;

                return ParseResponse<R>(response, expectedStatus);
            }
            catch (Exception ex)
            {
                throw new LoggedException("Error to process GET to Jira", ex);
            }
        }
    }
}