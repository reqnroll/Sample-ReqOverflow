using System;
using System.Net;
using AwesomeAssertions;
using ReqOverflow.Specs.API.Support;
using ReqOverflow.Web.Models;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Utils;

namespace ReqOverflow.Specs.API.Drivers;

public class AuthApiDriver(WebApiContext webApiContext, AuthApiDriver.LoginDriver login)
{
    public class LoginDriver(WebApiContext webApiContext) : ActionAttempt<LoginInputModel, string>
    {
        public event Action<LoginInputModel, string> OnAuthenticated;

        protected override string DoAction(LoginInputModel loginInput)
        {
            var response = webApiContext.ExecutePost<string>("api/auth", loginInput);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authToken = response.ResponseData;
            OnAuthenticated?.Invoke(loginInput, authToken);
            return authToken;
        }
    }

    public LoginDriver Login { get; } = login;

    public UserReferenceModel GetCurrentUser()
    {
        try
        {
            return webApiContext.ExecuteGet<UserReferenceModel>("/api/auth");
        }
        catch (HttpResponseException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}