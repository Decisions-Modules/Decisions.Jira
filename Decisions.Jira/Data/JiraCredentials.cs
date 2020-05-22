using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Decisions.Jira
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JiraConnectionType
    {
        [Description("Self Hosted Server")]
        JiraServer,
        [Description("Jira Cloud")]
        JiraCloud
    };

    [DataContract]
    public class JiraCredentials
    {
        [DataMember]
        [PropertyClassificationAttribute("User", 1)]
        public string User { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Password", 2)]
        public string Password { get; set; }
       
        [DataMember]
        [SelectStringEditorAttribute(new string[] { @"https://YOURNAME.atlassian.net"})]
        [PropertyClassificationAttribute("Jira URL", 3)]
        public string JiraURL { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Jira Connection Type", 4)]
        public JiraConnectionType JiraConnection { get; set; }
    }
}