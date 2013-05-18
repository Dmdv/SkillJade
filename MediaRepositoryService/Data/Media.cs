using System;
using System.IO;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Media
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid ForeignId { get; set; }

		[DataMember]
		public Stream Content { get; set; }

		[DataMember]
		public string Type { get; set; }
	}
}