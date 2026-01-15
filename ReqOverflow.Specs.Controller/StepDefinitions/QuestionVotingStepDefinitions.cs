using AwesomeAssertions;
using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionVotingStepDefinitions(
    VoteQuestionDriver voteQuestionDriver,
    IsolatedAuthContextFactory isolatedAuthContextFactory,
    QuestionContext questionContext)
{
    private QuestionSummaryModel _question;

    [When("the user votes {word} the question")]
    public void WhenTheUserVotesUpTheQuestion(VoteDirection vote)
    {
        _question = voteQuestionDriver.Perform(vote);
    }

    [When("the user attempts to vote {word} the question")]
    public void WhenTheUserAttemptsToVoteTheQuestion(VoteDirection vote)
    {
        _question = voteQuestionDriver.Perform(vote, true);
    }

    [Given("another user votes {word} the question in the meanwhile")]
    public void GivenAnotherUserVotesTheQuestionInTheMeanwhile(VoteDirection vote)
    {
        var otherUserAuthContext = isolatedAuthContextFactory.CreateAuthContext();
        otherUserAuthContext.Authenticate(DomainDefaults.AltUserName, DomainDefaults.AltUserPassword);
        var otherUserVoteDriver = isolatedAuthContextFactory.CreateDriver<VoteQuestionDriver>(otherUserAuthContext);
        otherUserVoteDriver.Perform(questionContext.CurrentQuestionId, vote);
    }

    [Then("the vote count of the question should be changed to {int}")]
    public void ThenTheVoteCountOfTheQuestionShouldBeChangedTo(int expectedVoteCount)
    {
        _question.Votes.Should().Be(expectedVoteCount);
    }

    [Then("the question voting attempt should fail with error {string}")]
    public void ThenTheQuestionVotingAttemptShouldFailWithError(string expectedErrorMessageKey)
    {
        voteQuestionDriver.ShouldFailWithError(expectedErrorMessageKey);
    }
}