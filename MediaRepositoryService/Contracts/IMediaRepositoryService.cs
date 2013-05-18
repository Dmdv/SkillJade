using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Faults;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface IMediaRepositoryService
	{
		[OperationContract]
		[FaultContract(typeof(UserNotFoundFault))]
		[FaultContract(typeof(EntityAlreadyExistsFault))]
		[FaultContract(typeof(InternalServerErrorFault))]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		void CreateDevice(string userName/*, string password, Guid deviceGuid, string deviceName*/);

		[OperationContract]
		[FaultContract(typeof(DeviceNotFoundFault))]
		[FaultContract(typeof(SerializeExceptionFault))]
		[FaultContract(typeof(EntityAlreadyExistsFault))]
		[FaultContract(typeof(InternalServerErrorFault))]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		void UploadPreview(Stream previewInfoStream);

		[OperationContract]
		[FaultContract(typeof(DeviceNotFoundFault))]
		[FaultContract(typeof(InternalServerErrorFault))]
		[FaultContract(typeof(SerializeExceptionFault))]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		void UploadOriginal(Stream fileInfoStream);
	}
}