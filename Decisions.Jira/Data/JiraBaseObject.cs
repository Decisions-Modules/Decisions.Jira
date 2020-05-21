﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data
{

    [DataContract]
    public class JiraBaseObject
    {

        [DataMember]
        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
    }

}