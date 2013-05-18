using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface IQuestionManager
	{
		[OperationContract(Name = "GetQuestion")]
		[WebInvoke(
			Method = "GET",
			BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "question/{id}")]
		Question GetQuestion(string id);

		[OperationContract(Name = "CreateQuestion")]
		[WebInvoke(
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json,
			RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/create/question")]
		void CreateQuestion(Question question);
	}
}