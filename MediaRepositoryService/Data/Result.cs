using System;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Result
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid UserId { get; set; }

		[DataMember]
		public bool Success { get; set; }

		[DataMember]
		public int Score { get; set; }
	}
}