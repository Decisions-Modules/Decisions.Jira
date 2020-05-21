using System.Runtime.Serialization;
using Decisions.Jira.Data.Project;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.Jira.Data.User
{
    [DataContract]
    public class JiraAssignProjectModel
    {
        [DataMember]
        [JsonIgnoreAttribute]
        [PropertyClassificationAttribute("ProjectId Or Key", 1)]
        public string ProjectIdOrKey { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "user")]
        [PropertyClassificationAttribute("Users", 2)]
        public string[] Users { get; set; }
        
        [DataMember]
        [JsonIgnoreAttribute]
        [PropertyClassificationAttribute("RoleId", 3)]
        public long RoleId { get; set; }
    }

    [DataContract]
    public class JiraAssignProjectResult : BaseJiraResult
    {
        [DataMember]
        public JiraProjectRolesResponseModel Data;

        public JiraAssignProjectResult() { }
        public JiraAssignProjectResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            Data = (JiraProjectRolesResponseModel)source.Data;
        }

    }
}
