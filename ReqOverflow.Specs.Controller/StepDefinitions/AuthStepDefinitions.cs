using System;
using FluentAssertions;
using ReqOverflow.Web.Models;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Support;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly AuthContext _authContext;
        private readonly AuthDriver _authDriver;

        public AuthStepDefinitions(AuthContext authContext, AuthDriver authDriver)
        {
            _authContext = authContext;
            _authDriver = authDriver;
        }

        [Given("user Marvin is authenticated")]
        [Given("the user is authenticated")]
        public void GivenTheUserIsAuthenticated()
        {
            _authContext.Authenticate(DomainDefaults.UserName, DomainDefaults.UserPassword);
        }

        [Given("the user is not authenticated")]
        public void GivenTheUserIsNotAuthenticated()
        {
            _authDriver.GetCurrentUser().Should().BeNull();
        }

        [When("the user attempts to log in with user name {string} and password {string}")]
        public void WhenTheUserAttemptsToLogInWithUserNameAndPassword(string userName, string password)
        {
            _authDriver.Login.Perform(
                new LoginInputModel {Name = userName, Password = password}, true);
        }

        [Then("the login attempt should be successful")]
        public void ThenTheLoginAttemptShouldBeSuccessful()
        {
            _authDriver.Login.ShouldBeSuccessful();
        }

        [Then("the user should be authenticated")]
        public void ThenTheUserShouldBeAuthenticated()
        {
            var currentUser = _authDriver.GetCurrentUser();
            currentUser.Should().NotBeNull();
            currentUser.Name.Should().Be(_authDriver.Login.LastInput.Name);
        }
    }
}
