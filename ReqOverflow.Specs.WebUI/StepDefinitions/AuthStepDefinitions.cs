using AwesomeAssertions;
using ReqOverflow.Specs.WebUI.Drivers;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.WebUI.StepDefinitions;

[Binding]
public class AuthStepDefinitions(
    AuthContext authContext,
    LoginPageDriver loginPageDriver,
    HomePageDriver homePageDriver)
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
        homePageDriver.GetCurrentUser().Should().BeNull();
    }

    [When("the user attempts to log in with user name {string} and password {string}")]
    public void WhenTheUserAttemptsToLogInWithUserNameAndPassword(string userName, string password)
    {
        loginPageDriver.Perform(
            new LoginInputModel {Name = userName, Password = password}, true);
    }

    [Then("the login attempt should be successful")]
    public void ThenTheLoginAttemptShouldBeSuccessful()
    {
        loginPageDriver.ShouldBeSuccessful();
    }

    [Then("the user should be authenticated")]
    public void ThenTheUserShouldBeAuthenticated()
    {
        var currentUser = homePageDriver.GetCurrentUser();
        currentUser.Should().NotBeNull();
        currentUser.Name.Should().Be(loginPageDriver.LastInput.Name);
    }
}