using DecisionsFramework.Design.Properties;
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
    public class JiraActorGroupModel
    {
        [DataMember]
        [PropertyClassificationAttribute("Name", 1)]
        public string Name { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }
    }

    [DataContract]
    public class JiraActorsModel
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Name", 1)]
        public string Name { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Type", 3)]
        public string Type { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Actor Group", 2)]
        public JiraActorGroupModel ActorGroup;
    }


    [DataContract]
    public class JiraScopeModel
    {
        [DataMember]
        [PropertyClassificationAttribute("Type", 2)]
        public string Type { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Project", 2)]
        public JiraIdReferenceModel Project { get; set; }

    }


    [DataContract]
    public class JiraProjectRolesResponseModel
    {
        [DataMember]
        [PropertyClassificationAttribute("Self", 1)]
        public string Self { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Name", 2)]
        public string Name { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Id", 3)]
        public long Id { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Description", 4)]
        public string Description { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Scope", 4)]
        public JiraScopeModel Scope { get; set; }

        [DataMember]
        public JiraActorsModel[] Actors { get; set; }
    }

    [DataContract]
    public class JiraProjectRolesResult : BaseJiraResult
    {
        [DataMember]
        public JiraProjectRolesResponseModel[] Data;

        public JiraProjectRolesResult() { }
        public JiraProjectRolesResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            HttpStatus = source.HttpStatus;

            var list = (List < JiraProjectRolesResponseModel >)source.Data;
            Data = list.ToArray();
        }
    }
}
