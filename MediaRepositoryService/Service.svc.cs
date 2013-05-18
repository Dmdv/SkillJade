using System;
using System.IO;
using DataAccess.BusinessLogic;
using MediaRepositoryWebRole.Contracts;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole
{
	public class MediaRepositoryService : ServiceBase,
		IMediaRepositoryService, 
		IUserManager,
		IAnswerManager,
		IQuestionManager,
		IResultManager,
		ITestManager,
		IAutorManager
	{
		private readonly UserManager _userManager;
		private readonly DeviceManager _deviceManager;
		private readonly MediaManager _mediaManager;

		public MediaRepositoryService()
		{
			var userContext = ServiceFactory.CreateUserContext();
			_userManager = new UserManager(
				userContext);

			_deviceManager = new DeviceManager(
				ServiceFactory.CreateDeviceContext(), 
				ServiceFactory.CreateUserContext());

			_mediaManager = new MediaManager(
				ServiceFactory.CreateMediaFileContext(),
				ServiceFactory.CreateQueryHistoryContext(),
				ServiceFactory.CreateDeviceContext(),
				ServiceFactory.CreateBlobRepository());
		}

		public void CreateDevice(string userName/*, string password, Guid deviceGuid, string deviceName*/)
		{
			//var task = _deviceManager.CreateDevice(deviceGuid, deviceName, userName, password);
			//return task.AsAsyncResult(callback, state);
		}

		public void UploadPreview(Stream previewInfoStream)
		{
			//var filStreamInfo = previewInfoStream.Deserialize();
			//var task = _mediaManager.UploadPreview(filStreamInfo.DeviceId, filStreamInfo.FileName, filStreamInfo.DateTime, filStreamInfo.Stream);
			//return task.AsAsyncResult(callback, state);
		}

		public void UploadOriginal(Stream fileInfoStream)
		{
			//var filStreamInfo = fileInfoStream.Deserialize();
			//var task = _mediaManager.UploadOriginal(filStreamInfo.DeviceId, filStreamInfo.FileName, filStreamInfo.DateTime, filStreamInfo.Stream);
			//return task.AsAsyncResult(callback, state);
		}

		//public IAsyncResult BeginCreateUser(Data.User user, AsyncCallback callback, object state)
		//{
		//	var task = _userManager.CreateUser(new User(user.Name, user.Password) {UserId = user.UserId});
		//	return task.AsAsyncResult(callback, state);
		//}

		//public void EndCreateUser(IAsyncResult result)
		//{
		//	ProcessWithExceptionShield(() => ((Task)result).Wait());
		//}

		public void CreateAnswer(Answer answer)
		{
		}

		public void CreateUser(User user)
		{
		}

		public void CreateAutor(Autor autor)
		{
		}

		public void CreateTest(Test test)
		{
		}

		public void CreateResult(Result result)
		{
		}

		public void CreateQuestion(Question question)
		{
		}

		public User GetUser(string id)
		{
			return new User {Name = "Dima", UserId = Guid.NewGuid()};
		}

		public Answer GetAnswer(string id)
		{
			return new Answer {QuesionId = Guid.NewGuid(), Text = "Sample text"};
		}

		public Question GetQuestion(string id)
		{
			return new Question {TestId = Guid.NewGuid(), Text = "Question text"};
		}

		public Result GetResult(string id)
		{
			return new Result {Score = 111, Success = true, UserId = Guid.NewGuid()};
		}

		public Test GetTest(string id)
		{
			return new Test
				{
					Description = "Description",
					AutorId = Guid.NewGuid(),
					Name = "Dima",
					LikeStat = 222,
					UsageStat = 555
				};
		}

		public Autor GetAutor(string id)
		{
			return new Autor {Name = "Autor", Id = Guid.NewGuid()};
		}
	}
}