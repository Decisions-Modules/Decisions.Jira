using DecisionsFramework.Design.Properties;
using System.Runtime.Serialization;

namespace Decisions.Jira.Data
{
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
    }
}