using ReqOverflow.Specs.API.Drivers;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.API.StepDefinitions;

[Binding]
public class RegistrationStepDefinitions(UserApiDriver userApiDriver)
{
    [Given("there is a user registered with user name {string} and password {string}")]
    public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
    {
        userApiDriver.Register.Perform(
            new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password });
    }

    [When("the user attempts to register with user name {string} and password {string}")]
    public void WhenTheUserAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
    {
        userApiDriver.Register.Perform(
            new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password },
            true);
    }

    [Then("the registration should be successful")]
    public void ThenTheRegistrationShouldBeSuccessful()
    {
        userApiDriver.Register.ShouldBeSuccessful();
    }
}