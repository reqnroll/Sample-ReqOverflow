using System;
using System.Linq;
using FluentAssertions;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;
using Reqnroll;
using Reqnroll.Assist;

namespace ReqOverflow.Specs.Controller.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly QuestionContext _questionContext;
        private readonly AuthContext _authContext;
        private HomePageModel _homePageModel;

        public HomeStepDefinitions(AuthContext authContext, QuestionContext questionContext)
        {
            _authContext = authContext;
            _questionContext = questionContext;
        }

        [When("the user checks the home page")]
        public void WhenTheUserChecksTheHomePage()
        {
            var controller = new HomeController();
            _homePageModel = controller.GetHomePageModel(_authContext.AuthToken);
        }

        [Then("the home page main message should be: {string}")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            _homePageModel.MainMessage.Should().Be(expectedMessage);
        }

        [Then("the user name of the user should be on the home page")]
        public void ThenTheUserNameOfTheUserShouldBeOnTheHomePage()
        {
            _authContext.IsLoggedIn.Should().BeTrue();
            _homePageModel.UserName.Should().Be(_authContext.LoggedInUserName);
        }

        [Then("the question should be listed among the latest questions as above")]
        public void ThenTheQuestionShouldBeListedAmongTheLatestQuestionsAsAbove()
        {
            var question = _homePageModel.LatestQuestions.FirstOrDefault(q => q.Id == _questionContext.CurrentQuestionId);
            _questionContext.QuestionSpecification.CompareToInstance(question.ToQuestionData());
        }

        [Then("the home page should contain the {int} latest questions ordered")]
        public void ThenTheHomePageShouldContainTheLatestQuestionsOrdered(int expectedCount)
        {
            var expectedQuestionIds = _questionContext.QuestionsCreated
                .OrderByDescending(q => q.AskedAt)
                .Take(expectedCount)
                .ToArray();
            _homePageModel.LatestQuestions.Should().Equal(expectedQuestionIds, (q1, q2) => q1.Id == q2.Id);
        }

    }
}
