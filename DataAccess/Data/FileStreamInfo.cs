using System;
using System.IO;

namespace DataAccess.Data
{
	[Serializable]
	public class FileStreamInfo
	{
		public Guid DeviceId { get; set; }

		public string FileName { get; set; }

		public DateTime DateTime { get; set; }

		public Stream Stream { get; set; }
	}
}