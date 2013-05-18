using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface IResultManager
	{
		[OperationContract(Name = "GetResult")]
		[WebInvoke(
			Method = "GET",
			BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "result/{id}")]
		Result GetResult(string id);

		[OperationContract(Name = "CreateResult")]
		[WebInvoke(
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json,
			RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/create/result")]
		void CreateResult(Result result);
	}
}