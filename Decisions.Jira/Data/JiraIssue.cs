using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{
    [DataContract]
    public class JiraIssue
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
        public JiraProjectReferenceModel JiraProject { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "issuetype")]
        [PropertyClassificationAttribute("Issue type", 4)]
        public JiraIssueTypeModel Issuetype { get; set; }
        
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        [RichTextInputEditorAttribute]
        [PropertyClassificationAttribute("Description", 5)]
        public string Description { get; set; }
    } 
}
