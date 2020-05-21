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
	public class IssueFieldsModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "required")]
		public bool Required { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "hasDefaultValue")]
		public bool HasDefaultValue { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "operations")]
		public string[] Operations { get; set; }
	}

	[DataContract]
	public class IssueFieldReferenceModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "issuetype")]
		public IssueFieldsModel Issuetype { get; set; }
	}

	[DataContract]
	public class IssueTypeModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "self")]
		public string Self { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "iconUrl")]
		public string IconUrl { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "subtask")]
		public bool Subtask { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "fields")]
		public IssueFieldReferenceModel Fields { get; set; }
	}

	[DataContract]
	public class JiraProjectMetadataModel : JiraBaseResponseModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "avatarUrls")]
		public JiraAvatarUrlsModel AvatarUrls { get; set; }

		[DataMember]
		[JsonProperty(PropertyName = "issuetypes")]
		public IssueTypeModel[] Issuetypes { get; set; }
	}

	[DataContract]
	public class JiraProjectMetadataResponseModel : JiraBaseResponseModel
	{
		[DataMember]
		[JsonProperty(PropertyName = "projects")]
		public JiraProjectMetadataModel[] Projects { get; set; }
	}

	[DataContract]
	public class JiraProjectMetadataResult : BaseJiraResult
	{
		[DataMember]
		public JiraProjectMetadataModel Data;

		public JiraProjectMetadataResult() { }
		public JiraProjectMetadataResult(JiraResultWithData source)
		{
			ErrorMessage = source.ErrorMessage;
			Status = source.Status;
			HttpStatus = source.HttpStatus;

			JiraProjectMetadataResponseModel projects = (JiraProjectMetadataResponseModel)source.Data;
			if (projects!=null && projects.Projects != null && projects.Projects.Length > 0)
			{
				Data = projects.Projects[0];
			}
		}
	}
}
