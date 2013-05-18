using System;
using System.Runtime.Serialization;

namespace MediaRepositoryWebRole.Data
{
	[DataContract]
	public class Autor
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public string Name { get; set; }
	}
}