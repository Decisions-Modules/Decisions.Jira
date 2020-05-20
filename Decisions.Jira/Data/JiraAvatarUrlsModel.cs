using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.Jira.Data
{
 	public class JiraAvatarUrlsModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "16x16")]
		public string Size16x16 { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "24x24")]
		public string Size24x24 { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "32x32")]
		public string Size32x32 { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "48x48")]
		public string Size48x48 { get; set; }
	}
}
