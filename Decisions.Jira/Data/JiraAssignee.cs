using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraAssignee
    {
        [DataMember]
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "issueIdOrKey")]
        public string IssueIdOrKey { get; set; }
    }
}
