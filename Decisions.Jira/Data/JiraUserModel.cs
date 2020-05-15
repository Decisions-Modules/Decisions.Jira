using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraUserModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "emailAddress")]
        [PropertyClassificationAttribute("Email Address", 1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "password")]
        [PropertyClassificationAttribute("Passwords", 4)]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 3)]
        public string Name { get; set; }
    }
}
