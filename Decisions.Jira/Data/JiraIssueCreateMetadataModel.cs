using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data
{
    [DataContract]
    class JiraIssueCreateMetadataModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "projects")]
        public JiraProjectMetadataModel[] Projects { get; set; }


    }

    [DataContract]
    public class JiraProjectMetadataModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        [PropertyClassificationAttribute("Id", 1)]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        [PropertyClassificationAttribute("Key", 2)]
        public string Key { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 3)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "issuetypes")]
        [PropertyClassificationAttribute("Issue Types", 4)]
        public JiraIssueTypeModel[] Issuetypes { get; set; }

    }

    [DataContract]
    public class JiraIssueTypeModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        [PropertyClassificationAttribute("Id", 1)]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 2)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "subtask")]
        public Boolean Subtask { get; set; }
    }
}
