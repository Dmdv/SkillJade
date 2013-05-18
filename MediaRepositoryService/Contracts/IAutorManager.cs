using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface IAutorManager
	{
		[OperationContract(Name = "GetAutor")]
		[WebInvoke(
			Method = "GET",
			BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "autor/{id}")]
		Autor GetAutor(string id);

		[OperationContract(Name = "CreateAutor")]
		[WebInvoke(
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json,
			RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/create/autor")]
		void CreateAutor(Autor autor);
	}
}