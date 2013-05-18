using System;
using System.Runtime.Serialization;

namespace DataAccess.Data
{
	[DataContract]
	public class User
	{
		[DataMember]
		public Guid UserId { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Password { get; set; }
	}
}