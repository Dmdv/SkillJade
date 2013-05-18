using System;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Question
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid TestId { get; set; }

		[DataMember]
		public string Text { get; set; }

		[DataMember]
		public byte[] Image { get; set; }
	}
}