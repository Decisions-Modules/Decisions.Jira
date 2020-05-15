using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraAssignee
    {
        [DataMember]
        [JsonProperty(PropertyName = "accountId")]
        [PropertyClassificationAttribute("AccountId", 1)]
        public string AccountId { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "issueIdOrKey")]
        [PropertyClassificationAttribute("IssueId Or Key", 2)]
        public string IssueIdOrKey { get; set; }
    }
}
