using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Decisions.Jira.Data;
using DecisionsFramework.ServiceLayer;

namespace Decisions.Jira
{
    public class Utility
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
    }
}