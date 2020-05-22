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
    public class JiraBaseResponseModel
    {

        [DataMember]
        public string Self { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Key { get; set; }
    }

}
