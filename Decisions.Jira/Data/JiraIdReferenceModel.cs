using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.Jira
{
    [DataContract]
    public class JiraIdReferenceModel
    {
        [DataMember]
        public string Id { get; set; }
        
        public JiraIdReferenceModel(string id)
        {
             Id = id;
        }
        public JiraIdReferenceModel()
        {}


    }
}
