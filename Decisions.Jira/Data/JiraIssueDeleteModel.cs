using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIssueDeleteModel
    {
        [DataMember]
         [JsonIgnore]
        public string IssueIdOrKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "deleteSubtasks")]
        public bool DeleteSubtasks { get; set; }
    }
}
