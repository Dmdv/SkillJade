﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DataAccess.Repository
{
	public class BlobRepository : IBlobRepository
	{
		private readonly CloudBlobClient _blobClient;

		public BlobRepository(string connectionString)
		{
			var storageAccount = StorageAccountFactory.CreateCloudStgrageAccount(connectionString);
			_blobClient = storageAccount.CreateCloudBlobClient();
		}

		public Task SaveBlob(string containerName, string fileName, Stream stream)
		{
			CloudBlockBlob cloudBlob = null;
			try
			{
				if (stream.CanSeek)
				{
					stream.Seek(0, SeekOrigin.Begin);
				}
				
				var blobContainer = CreateContainer(containerName);
				cloudBlob = CreateBlockBlob(fileName, blobContainer);
				return Task.Factory.FromAsync(cloudBlob.BeginUploadFromStream, cloudBlob.EndUploadFromStream, stream, null);
			}
			catch (StorageException exception)
			{
				if (cloudBlob != null)
				{
					cloudBlob.DeleteIfExists();
				}
				throw new InvalidOperationException(
					string.Format(
						"Exception on read value from blob. Container: {0}, blob name: {1}, message {2}",
						containerName,
						fileName,
						exception.Message),
					exception);
			}
		}

		private static CloudBlockBlob CreateBlockBlob(string fileName, CloudBlobContainer blobContainer)
		{
			return blobContainer.GetBlockBlobReference(fileName);
		}

		private CloudBlobContainer CreateContainer(string containerName)
		{
			var container = _blobClient.GetContainerReference(containerName);
			if (container.CreateIfNotExists())
			{
				var perm = new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob};
				container.SetPermissions(perm);
			}

			return container;
		}
	}
}