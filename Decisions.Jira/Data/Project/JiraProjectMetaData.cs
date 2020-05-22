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
	public class IssueFieldsModel
	{
		[DataMember]
		public bool Required { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Key { get; set; }

		[DataMember]
		public bool HasDefaultValue { get; set; }

		[DataMember]
		public string[] Operations { get; set; }
	}

	[DataContract]
	public class IssueFieldReferenceModel
	{
		[DataMember]
		public IssueFieldsModel Issuetype { get; set; }
	}

	[DataContract]
	public class IssueTypeModel
	{
		[DataMember]
		public string Self { get; set; }

		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string IconUrl { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public bool Subtask { get; set; }

		[DataMember]
		public IssueFieldReferenceModel Fields { get; set; }
	}

	[DataContract]
	public class JiraProjectMetadataModel : JiraBaseResponseModel
	{
		[DataMember]
		public JiraAvatarUrlsModel AvatarUrls { get; set; }

		[DataMember]
		public IssueTypeModel[] Issuetypes { get; set; }
	}

	[DataContract]
	public class JiraProjectMetadataResponseModel : JiraBaseResponseModel
	{
		[DataMember]
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
