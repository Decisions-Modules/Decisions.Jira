﻿using DecisionsFramework.Design.Properties;
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
    public class JiraIssueDeleteModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("IssueId or Key", 1)]
        public string IssueIdOrKey { get; set; }

        [DataMember]
        [PropertyClassificationAttribute("Delete Subtasks", 2)]
        public bool DeleteSubtasks { get; set; }
    }


}
