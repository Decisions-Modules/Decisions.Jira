using Decisions.Jira;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectLead { PROJECT_LEAD, UNASSIGNED };

    [DataContract]
    public class JiraProjectModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("ProjectId or Key", 1)]
        public string ProjectIdOrKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 2)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        [PropertyClassificationAttribute("Key", 3)]
        public string Key { get; set; }

        [DataMember]
        [JsonIgnore]
        [JsonProperty(PropertyName = "projectTypeKey")]
        [SelectStringEditorAttribute(new string[] { "software", "service_desk", "business" })]
        [PropertyClassificationAttribute("Project Type Key", 4)]
        public string ProjectTypeKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "permissionScheme")]
        [PropertyClassificationAttribute("Permission Scheme", 5)]
        public long? PermissionScheme { get; set; }

        [PropertyHidden(true)]
        [JsonIgnore]
        public string[] AvailableProjectTemplateKeys { get { return JiraUtility.AvailableProjectTemplateKeys; } }

        [DataMember]
        [JsonProperty(PropertyName = "projectTemplateKey")]
        [SelectStringEditorAttribute("AvailableProjectTemplateKeys",SelectStringEditorType.DropdownList,false)]

        [PropertyClassificationAttribute("Project Template Key", 6)]
        public string ProjectTemplateKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "description")]
        [RichTextInputEditorAttribute]
        [PropertyClassificationAttribute("Description", 7)]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "notificationScheme")]
        [PropertyClassificationAttribute("Notification Scheme", 8)]
        public long? NotificationScheme { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "leadAccountId")]
        [PropertyClassificationAttribute("Lead Account Id (Only for Jira Cloud)", 9)]
        public string LeadAccountId { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "lead")]
        [PropertyClassificationAttribute("Lead Name (Only for Jira Server)", 9)]
        public string Lead { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "assigneeType")]
        [PropertyClassificationAttribute("Assignee Type", 10)]
        public ProjectLead? AssigneeType { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "categoryId")]
        [PropertyClassificationAttribute("Category Id", 11)]
        public long? CategoryId { get; set; }
    }

    [DataContract]
    public class JiraCreateProjectResult : BaseJiraResult
    {
        [DataMember]
        public JiraBaseResponseModel Data;

        public JiraCreateProjectResult() { }
        public JiraCreateProjectResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            HttpStatus = source.HttpStatus;
            Data = (JiraBaseResponseModel)source.Data;
        }

    }
}
