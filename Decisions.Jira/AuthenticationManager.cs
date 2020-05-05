using System;
using System.Net.Http.Headers;
using System.Text;
using Decisions.Jira.Data;

namespace Decisions.Jira
{
    public class AuthenticationManager
    {
        public AuthenticationManager()
        { 
        }
        public AuthenticationHeaderValue GetAuthHeader(JiraCredentials creds)
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(creds.User + ":" + creds.Password)));
        }
    }
}