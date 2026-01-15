using System.Net;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;
using ReqOverflow.Web.Utils;

namespace ReqOverflow.Specs.Controller.Drivers;

public class AuthDriver(LoginDriver login, AuthContext authContext)
{
    public LoginDriver Login { get; } = login;

    public UserReferenceModel GetCurrentUser()
    {
        var controller = new AuthController();
        try
        {
            return controller.GetCurrentUser(authContext.AuthToken);
        }
        catch (HttpResponseException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}