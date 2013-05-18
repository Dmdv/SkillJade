using System;
using System.Runtime.Serialization;

namespace DataAccess.Data
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