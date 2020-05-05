using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIssue
    {
        [DataMember]
        [JsonIgnore]
        public string IssueIdOrKey { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "summary")]
        public string Details { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "project")]
        public JiraProjectReferenceModel JiraProject { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "issuetype")]
        public JiraIssueTypeModel Issuetype { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    } 
}
