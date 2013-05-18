using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface ITestManager
	{
		[OperationContract(Name = "GetTest")]
		[WebInvoke(
			Method = "GET",
			BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "test/{id}")]
		Test GetTest(string id);

		[OperationContract(Name = "CreateTest")]
		[WebInvoke(
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json,
			RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/create/test")]
		void CreateTest(Test test);
	}
}