using System.ServiceModel;
using System.ServiceModel.Web;
using MediaRepositoryWebRole.Data;

namespace MediaRepositoryWebRole.Contracts
{
	[ServiceContract]
	public interface IUserManager
	{
		[OperationContract(Name = "GetUser")]
		[WebInvoke(
			Method = "GET",
			BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json, 
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "user/{id}")]
		User GetUser(string id);

		[OperationContract(Name = "CreateUser")]
		[WebInvoke(
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json,
			RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/create/user")]
		void CreateUser(User user);
	}
}