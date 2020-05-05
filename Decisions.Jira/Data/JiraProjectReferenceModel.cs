using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Decisions.Jira.Data
{
    [DataContract]
    public    class JiraProjectReferenceModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }
        
        public JiraProjectReferenceModel(string id)
        {
            this.Id = id;
        }
    }
}
