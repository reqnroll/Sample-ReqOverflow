using AwesomeAssertions;
using ReqOverflow.Web.Models;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Support;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class AuthStepDefinitions(AuthContext authContext, AuthDriver authDriver)
{
    [Given("user Marvin is authenticated")]
    [Given("the user is authenticated")]
    public void GivenTheUserIsAuthenticated()
    {
        authContext.Authenticate(DomainDefaults.UserName, DomainDefaults.UserPassword);
    }

    [Given("the user is not authenticated")]
    public void GivenTheUserIsNotAuthenticated()
    {
        authDriver.GetCurrentUser().Should().BeNull();
    }

    [When("the user attempts to log in with user name {string} and password {string}")]
    public void WhenTheUserAttemptsToLogInWithUserNameAndPassword(string userName, string password)
    {
        authDriver.Login.Perform(
            new LoginInputModel {Name = userName, Password = password}, true);
    }

    [Then("the login attempt should be successful")]
    public void ThenTheLoginAttemptShouldBeSuccessful()
    {
        authDriver.Login.ShouldBeSuccessful();
    }

    [Then("the user should be authenticated")]
    public void ThenTheUserShouldBeAuthenticated()
    {
        var currentUser = authDriver.GetCurrentUser();
        currentUser.Should().NotBeNull();
        currentUser.Name.Should().Be(authDriver.Login.LastInput.Name);
    }
}