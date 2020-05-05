using System.Runtime.Serialization;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraCredentials
    {
        
        [DataMember] public string User { get; set; }
        [DataMember] public string Password { get; set; }
        [DataMember] public string JiraURL { get; set; }
    }
}