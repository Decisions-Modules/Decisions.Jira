using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraAssignProjectModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "projectIdOrKey")]
        public string ProjectIdOrKey { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "user")]
        public string[] Users { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "role")]
        public int RoleId { get; set; }
    }
}
