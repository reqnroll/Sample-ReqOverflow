using System.Linq;
using AwesomeAssertions;
using ReqOverflow.Specs.WebUI.Drivers;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Web.Models;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using Reqnroll;

namespace ReqOverflow.Specs.WebUI.StepDefinitions;

[Binding]
public class HomeStepDefinitions(
    AuthContext authContext,
    HomePageDriver homePageDriver,
    QuestionContext questionContext)
{
    private HomePageModel _homePageModel;

    [When("the client checks the home page")]
    public void WhenTheClientChecksTheHomePage()
    {
        _homePageModel = homePageDriver.GetHomePageModel();
    }

    [When("the user checks the home page")]
    public void WhenTheUserChecksTheHomePage()
    {
        _homePageModel = homePageDriver.GetHomePageModel();
    }

    [Then("the home page main message should be: {string}")]
    public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
    {
        _homePageModel.MainMessage.Should().Be(expectedMessage);
    }

    [Then("the user name of the user should be on the home page")]
    public void ThenTheUserNameOfTheUserShouldBeOnTheHomePage()
    {
        authContext.IsLoggedIn.Should().BeTrue();
        _homePageModel.UserName.Should().Be(authContext.LoggedInUserName);
    }

    [Then("the question should be listed among the latest questions as above")]
    public void ThenTheQuestionShouldBeListedAmongTheLatestQuestionsAsAbove()
    {
        var question = _homePageModel.LatestQuestions.FirstOrDefault(q => q.Id == questionContext.CurrentQuestionId);
        questionContext.QuestionSpecification.CompareToInstance(question.ToQuestionData());
    }

    [Then("the home page should contain the {int} latest questions ordered")]
    public void ThenTheHomePageShouldContainTheLatestQuestionsOrdered(int expectedCount)
    {
        var expectedQuestionIds = questionContext.QuestionsCreated
            .OrderByDescending(q => q.AskedAt)
            .Take(expectedCount)
            .ToArray();
        _homePageModel.LatestQuestions.Should().Equal(expectedQuestionIds, (q1, q2) => q1.Id == q2.Id);
    }
}