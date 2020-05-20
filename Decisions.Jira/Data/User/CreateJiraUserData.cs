using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data.User
{
    [DataContract]
    public class JiraUserModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "emailAddress")]
        [PropertyClassificationAttribute("Email Address", 1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "password")]
        [PropertyClassificationAttribute("Passwords", 4)]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 3)]
        public string Name { get; set; }
    }

    [DataContract]
    public class JiraCreateUserResponseModel
    {
        [DataMember]
        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "emailAddress")]
        [PropertyClassificationAttribute("Email Address", 1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 3)]
        public string Name { get; set; }

    }

    [DataContract]
    public class JiraCreateUserResult : BaseJiraResult
    {
        [DataMember]
        public JiraCreateUserResponseModel Data;

        public JiraCreateUserResult() { }
        public JiraCreateUserResult(JiraResultWithData source)
        {
            ErrorMessage = source.ErrorMessage;
            Status = source.Status;
            Data = (JiraCreateUserResponseModel)source.Data;
        }

    }
}
