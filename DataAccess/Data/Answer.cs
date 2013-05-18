using System;
using System.Runtime.Serialization;

namespace DataAccess.Data
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