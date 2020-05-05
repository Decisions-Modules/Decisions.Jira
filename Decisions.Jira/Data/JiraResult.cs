using System.Net;
using System.Runtime.Serialization;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraResult
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public HttpStatusCode Status{ get; set; }
        
        [DataMember]
        public object Data;
    }
}
