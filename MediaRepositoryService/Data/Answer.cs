using System;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Answer
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid QuesionId { get; set; }

		[DataMember]
		public string Text { get; set; }
	}
}