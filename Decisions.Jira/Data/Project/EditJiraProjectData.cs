using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira
{
    [DataContract]
    public class JiraEditProjectResponseModel : JiraBaseResponseModel
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
            HttpStatus = source.HttpStatus;
            Data = (JiraEditProjectResponseModel)source.Data;
        }
    }

}
