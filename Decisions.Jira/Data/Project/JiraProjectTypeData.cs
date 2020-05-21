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
	public class JiraProjectTypeResponseModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "formattedKey")]
		public string FormattedKey { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "descriptionI18nKey")]
		public string DescriptionI18nKey { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "color")]
		public string Color { get; set; }
	}

	[DataContract]
	public class JiraProjectTypeResult : BaseJiraResult
	{
		[DataMember]
		public JiraProjectTypeResponseModel Data;

		public JiraProjectTypeResult() { }
		public JiraProjectTypeResult(JiraResultWithData source)
		{
			ErrorMessage = source.ErrorMessage;
			Status = source.Status;
			HttpStatus = source.HttpStatus;
			Data = (JiraProjectTypeResponseModel)source.Data;
		}
	}
}
