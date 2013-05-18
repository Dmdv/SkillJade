using System;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Test
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid AutorId { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public int LikeStat { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int UsageStat { get; set; }
	}
}