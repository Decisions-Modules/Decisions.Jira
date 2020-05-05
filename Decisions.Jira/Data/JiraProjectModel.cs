using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraProjectModel
    {
        [DataMember]
        [JsonIgnore]
        public string ProjectIdOrKey { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [DataMember]
        [JsonIgnore]
        [JsonProperty(PropertyName = "projectTypeKey")]
        public string ProjectTypeKey { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "permissionScheme")]
        public long? PermissionScheme { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "projectTemplateKey")]
        public string ProjectTemplateKey { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "notificationScheme")]
        public long? NotificationScheme { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "leadAccountId")]
        public string LeadAccountId { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "assigneeType")]
        public string AssigneeType { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "categoryId")]
        public long? CategoryId { get; set; }


    }
}
