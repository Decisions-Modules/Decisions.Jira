using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data.Issue
{
    [DataContract]
    public class JiraAssigneeModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "accountId")]
        [PropertyClassificationAttribute("User AccountId", 1)]
        public string AccountId { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        [PropertyClassificationAttribute("User Key", 2)]
        public string Key { get; set; }

        [DataMember]
        [JsonIgnoreAttribute]
        [PropertyClassificationAttribute("IssueId Or Key", 3)]
        public string IssueIdOrKey { get; set; }
    }

}
