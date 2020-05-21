using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Decisions.Jira.Data
{
    /*[DataContract]
    public class JiraResult
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public HttpStatusCode Status{ get; set; }
        
        [DataMember]
        public object Data;
    }*/

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JiraResultStatus { Fail = 0, Success = 1}

    [DataContract]
    public class BaseJiraResult
    {
        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public JiraResultStatus Status { get; set; }

        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

    }

    public class JiraResultWithData: BaseJiraResult
    {
        [DataMember]
        public object Data;
    }
}
