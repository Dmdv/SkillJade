using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace DataAccess.Repository
{
	internal class StorageAccountFactory
	{
		public static CloudStorageAccount CreateCloudStgrageAccount(string connectionString)
		{
			var s = CloudStorageAccount.DevelopmentStorageAccount.ToString();
			return s == connectionString
				       ? CloudStorageAccount.DevelopmentStorageAccount
				       : CloudStorageAccount.Parse(connectionString);
		}
	}
}
