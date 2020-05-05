using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraUserModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
