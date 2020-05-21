using Decisions.Jira.Data.Project;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Decisions.Jira.Data.Issue
{
    public class JiraIssueModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("IssueId or Key", 1)]
        public string IssueIdOrKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "summary")]
        [PropertyClassificationAttribute("Details", 2)]
        public string Details { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "project")]
        [PropertyClassificationAttribute("Jira Project", 3)]
        public JiraIdReferenceModel JiraProject { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "issuetype")]
        [PropertyClassificationAttribute("Issue type", 4)]
        public JiraIdReferenceModel Issuetype { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "description")]
        [RichTextInputEditorAttribute]
        [PropertyClassificationAttribute("Description", 5)]
        public string Description { get; set; }
    }

    [DataContract]
    public class JiraCreateIssueResult : BaseJiraResult
    {
        [DataMember]
        public JiraBaseResponseModel Data;

        public JiraCreateIssueResult() { }
        public JiraCreateIssueResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            HttpStatus = source.HttpStatus;
            Data = (JiraBaseResponseModel)source.Data;
        }

    }
}
