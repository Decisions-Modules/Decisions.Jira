using Decisions.Jira;
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

namespace Decisions.Jira
{
    public class JiraIssueModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("IssueId or Key", 1)]
        public string IssueIdOrKey { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Details", 2)]
        public string Summary { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Jira Project", 3)]
        public JiraIdReferenceModel Project { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Issue type", 4)]
        public JiraIdReferenceModel Issuetype { get; set; }

        [DataMember]
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
