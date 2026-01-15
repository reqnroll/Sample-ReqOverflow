using ReqOverflow.Specs.WebUI.Drivers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Support;

public class AuthContext
{
    private readonly LoginPageDriver _loginPageDriver;

    public string LoggedInUserName { get; set; }

    public bool IsLoggedIn => LoggedInUserName != null;

    public AuthContext(LoginPageDriver loginPageDriver)
    {
        _loginPageDriver = loginPageDriver;

        loginPageDriver.OnAuthenticated += (loginInput, _) =>
        {
            LoggedInUserName = loginInput.Name;
        };
    }

    public void Authenticate(string userName, string password)
    {
        _loginPageDriver.Perform(new LoginInputModel { Name = userName, Password = password });
    }
}