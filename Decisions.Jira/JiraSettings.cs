using System.Collections.Generic;
using System.Runtime.Serialization;
using Decisions.Jira.Data;
using DecisionsFramework.Data.ORMapper;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using DecisionsFramework.ServiceLayer;
using DecisionsFramework.ServiceLayer.Actions;
using DecisionsFramework.ServiceLayer.Actions.Common;
using DecisionsFramework.ServiceLayer.Utilities;

namespace Decisions.Jira
{
    [ORMEntity]
    [DataContract]
    public class JiraSettings : AbstractModuleSettings, IInitializable
    {
        [ORMField(typeof(KeyFieldConverter))]
        [DataMember]
        public string JiraURL { get; set; }

        [ORMField]
        [DataMember]
        public string UserId { get; set; }

        [ORMField(typeof(EncryptedConverter))]
        [DataMember]
        [PasswordText]
        public string Password { get; set; }

        [ORMField]
        [DataMember]
        [PropertyClassificationAttribute("Jira Connection Type", 4)]
        public JiraConnectionType JiraConnection { get; set; }

        public override BaseActionType[] GetActions(AbstractUserContext userContext, EntityActionType[] types)
        {
            List<BaseActionType> all = new List<BaseActionType>();
            all.Add(new EditEntityAction(typeof(JiraSettings), "Edit", "Edit"));
            return all.ToArray();
        }

        public void Initialize()
        {
            JiraSettings me = ModuleSettingsAccessor<JiraSettings>.GetSettings();
            if (string.IsNullOrEmpty(Id))
            {
                ModuleSettingsAccessor<JiraSettings>.SaveSettings(); 
            }
        }
    }
}