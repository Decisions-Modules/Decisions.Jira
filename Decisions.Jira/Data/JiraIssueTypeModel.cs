using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIssueTypeModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        public JiraIssueTypeModel(string id)
        {
             Id = id;
        }

        
    }
}
