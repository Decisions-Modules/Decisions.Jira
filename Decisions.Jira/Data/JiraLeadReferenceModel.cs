using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraLeadReferenceModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }
    }
}
