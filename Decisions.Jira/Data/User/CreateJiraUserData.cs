using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira
{
    [DataContract]
    public class JiraUserModel
    {
        [DataMember]
        [PropertyClassificationAttribute("Email Address", 1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Passwords", 4)]
        public string Password { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Name", 3)]
        public string Name { get; set; }
    }

    [DataContract]
    public class JiraCreateUserResponseModel
    {
        [DataMember]
        public string Self { get; set; }

        [DataMember]
        public string AccountId { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Email Address", 1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Display Name", 2)]
        public string DisplayName { get; set; }

        [DataMember]
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
