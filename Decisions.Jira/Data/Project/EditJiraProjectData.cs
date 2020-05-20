using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data.Project
{
    [DataContract]
    public class JiraEditProjectResponseModel : JiraBaseObject
    {
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }

    [DataContract]
    public class JiraEditProjectResult : BaseJiraResult
    {
        [DataMember]
        public JiraEditProjectResponseModel Data;

        public JiraEditProjectResult() { }
        public JiraEditProjectResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            Data = (JiraEditProjectResponseModel)source.Data;
        }
    }

}
