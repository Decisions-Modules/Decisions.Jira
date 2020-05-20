using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data.Project
{

    [DataContract]
    public class JiraActorGroupModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 1)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }
    }

    [DataContract]
    public class JiraActorsModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 1)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "type")]
        [PropertyClassificationAttribute("Type", 3)]
        public string Type { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "actorGroup")]
        [PropertyClassificationAttribute("Actor Group", 2)]
        public JiraActorGroupModel ActorGroup;
    }


    [DataContract]
    public class JiraScopeModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "type")]
        [PropertyClassificationAttribute("Type", 2)]
        public string Type { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "project")]
        [PropertyClassificationAttribute("Project", 2)]
        public JiraIdReferenceModel Project { get; set; }

    }


        [DataContract]
    public class JiraProjectRolesResponseModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "self")]
        [PropertyClassificationAttribute("Self", 1)]
        public string Self { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 2)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "id")]
        [PropertyClassificationAttribute("Id", 3)]
        public long Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "description")]
        [PropertyClassificationAttribute("Description", 4)]
        public string Description { get; set; }


        [DataMember]
        [JsonProperty(PropertyName = "scope")]
        [PropertyClassificationAttribute("Scope", 4)]
        public JiraScopeModel Scope { get; set; }


        [DataMember]
        [JsonProperty(PropertyName = "actors")]
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

            var list = (List < JiraProjectRolesResponseModel >)source.Data;
            Data = list.ToArray();
        }
    }
}
