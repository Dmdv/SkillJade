using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repository;
using MediaRepositoryWebRole;
using MediaRepositoryWebRole.Contracts;
using MediaRepositoryWebRole.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Blob;
using DataAccess.Test.Extensions;
using User = MediaRepositoryWebRole.Data.User;

namespace DataAccess.Test
{
	[TestClass]
	[DeploymentItem(@"..\..\..\DataAccess.Test\trace.jpg")]
	[DeploymentItem(@"..\..\..\DataAccess.Test\Image.jpg")]
	public class ServiceTests
	{
		private const string Image = "Image.jpg";
		private const string FileName = "trace.jpg";

		private static IMediaRepositoryService _mediaRepository;
		private static IUserManager _userManager;

		private User _user;
		private static ITableStorageProvider<Entities.User> _userContext;
		private static ITableStorageProvider<Device> _deviceContext;
		private static ITableStorageProvider<MediaFile> _mediaContext;
		private static CloudBlobClient _blobClient;

		private string _deviceName;
		private Guid _deviceId;

		[ClassInitialize]
		public static void Init(TestContext context)
		{
			TestsHelper.InitializeAzure();
			_userManager = new ServiceInterfaceProxy<IUserManager>(string.Empty).GetInterface();
			_mediaRepository = new ServiceInterfaceProxy<IMediaRepositoryService>(string.Empty).GetInterface();
			_userContext = ServiceFactory.CreateUserContext();
			_deviceContext = ServiceFactory.CreateDeviceContext();
			_mediaContext = ServiceFactory.CreateMediaFileContext();
			_blobClient = TestsHelper.InitializeAzure();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_user = new User
			{
				UserId = Guid.NewGuid(),
				Name = Guid.NewGuid().ToString(),
				Password = Guid.NewGuid().ToString()
			};

			_deviceName = Guid.NewGuid().ToString();
			_deviceId = Guid.NewGuid();
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestUserManager()
		{
			var svc = new MediaRepositoryService();
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateUserRemotely()
		{
			_userManager.GetUser("11");

			_userManager.CreateUser(_user);

			var repository = new UserRepository(_userContext);
			Assert.IsTrue(repository.IsExist(_user.Name, _user.Password, _user.UserId));
			Assert.IsFalse(repository.IsExist(_user.Name, "111", _user.UserId));
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateUserTwiceRemotely()
		{
			_userManager.CreateUser(_user);
			try
			{
				_userManager.CreateUser(_user);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.InnerException.Message.ToLower().Contains("entity already exists"));
			}
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateDeviceUserNotFound()
		{
			try
			{
				CreateDevice();
			}
			catch (AggregateException ex)
			{
				var innerException = ex.InnerException.Message.ToLower();
				Assert.IsTrue(innerException.Contains("user") && innerException.Contains("does not exist"));
			}
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateDeviceTwiceRemotely()
		{
			_userManager.CreateUser(_user);
			CreateDevice();
			try
			{
				CreateDevice();
			}
			catch (Exception e)
			{
				var message = string.Format("Device {0} already exists", _deviceName);
				Assert.IsTrue(e.InnerException.Message == message);
			}
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateDeviceAndCheck()
		{
			_userManager.CreateUser(_user);
			CreateDevice();
			var repository = new DeviceRepository(_deviceContext);
			Assert.IsTrue(repository.IsExist(_deviceId));
			Assert.IsNotNull(repository.Find(_deviceId, _deviceName).Result.FirstOrDefault());
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestUploadPreviewDeviceNotFound()
		{
			try
			{
				UploadPreview(Image);
			}
			catch (AggregateException ex)
			{
				var message = ex.InnerException.Message;
				Assert.IsTrue(message.Contains("Device with id"));
				Assert.IsTrue(message.Contains("hasn't been found."));
			}
		}

		[TestCategory("ServiceTests")]
		[TestMethod]
		public void TestCreateDeviceAndUploadPreview()
		{
			_userManager.CreateUser(_user);
			CreateDevice();
			UploadPreview(Image);

			var blobContainer = _blobClient.GetContainerReference(_deviceId.ToString());
			Assert.AreEqual(blobContainer.Name, _deviceId.ToString());
		}

		private void UploadPreview(string fileName)
		{
			using (var memoryStream = new MemoryStream(File.ReadAllBytes(fileName)))
			{
				using (var writeStream = new MemoryStream())
				{
					var preview = new FileStreamInfo
						{
							DateTime = DateTime.Now,
							FileName = fileName,
							DeviceId = _deviceId,
							Stream = memoryStream
						};

					var formatter = new BinaryFormatter();
					formatter.Serialize(writeStream, preview);

					if (writeStream.CanSeek)
					{
						writeStream.Seek(0, SeekOrigin.Begin);
					}

					_mediaRepository.UploadPreview(writeStream);
				}
			}
		}

		private void CreateDevice()
		{
			_mediaRepository.CreateDevice(
				_user.Name/*,
				_user.Password,
				_deviceId,
				_deviceName*/);
		}
	}
}