using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class RegistrationStepDefinitions(UserRegistrationDriver userRegistrationDriver)
{
    [Given("there is a user registered with user name {string} and password {string}")]
    public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
    {
        userRegistrationDriver.Perform(new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password });
    }

    [When("the user attempts to register with user name {string} and password {string}")]
    public void WhenTheUserAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
    {
        userRegistrationDriver.Perform(
            new RegisterInputModel {UserName = userName, Password = password, PasswordReEnter = password}, 
            true);
    }

    [Then("the registration should be successful")]
    public void ThenTheRegistrationShouldBeSuccessful()
    {
        userRegistrationDriver.ShouldBeSuccessful();
    }
}