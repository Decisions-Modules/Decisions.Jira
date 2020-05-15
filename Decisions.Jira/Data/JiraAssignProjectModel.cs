using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraAssignProjectModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "projectIdOrKey")]
        [PropertyClassificationAttribute("ProjectId Or Key", 1)]
        public string ProjectIdOrKey { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "user")]
        [PropertyClassificationAttribute("Users", 2)]
        public string[] Users { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "role")]
        [PropertyClassificationAttribute("RoleId", 3)]
        public int RoleId { get; set; }
    }
}
