using ReqOverflow.Specs.API.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.API.Drivers;

public class UserApiDriver
{
    public ActionAttempt<RegisterInputModel, UserReferenceModel> Register { get; }

    public UserApiDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
    {
        Register = actionAttemptFactory.CreateWithStatusCheck<RegisterInputModel, UserReferenceModel>(
            nameof(Register),
            registerInput => webApiContext.ExecutePost<UserReferenceModel>("/api/user", registerInput));
    }
}