using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIssueDeleteModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("IssueId or Key", 1)]
        public string IssueIdOrKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "deleteSubtasks")]
        [PropertyClassificationAttribute("Delete Subtasks", 2)]
        public bool DeleteSubtasks { get; set; }
    }
}
