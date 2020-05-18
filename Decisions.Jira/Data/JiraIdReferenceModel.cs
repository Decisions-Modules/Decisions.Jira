using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIdReferenceModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        public JiraIdReferenceModel(string id)
        {
             Id = id;
        }
        public JiraIdReferenceModel()
        {}


    }
}
