using AwesomeAssertions;
using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class AnswerVotingStepDefinitions(
    VoteAnswerDriver voteAnswerDriver,
    QuestionContext questionContext,
    QuestionDetailsPageDriver questionDetailsPageDriver,
    IsolatedAuthContextFactory isolatedAuthContextFactory)
{
    [When("the user votes {word} the answer")]
    public void WhenTheUserVotesTheAnswer(VoteDirection vote)
    {
        voteAnswerDriver.Perform(vote);
    }

    [When("the user attempts to vote {word} the answer")]
    public void WhenTheUserAttemptsToVoteTheAnswer(VoteDirection vote)
    {
        voteAnswerDriver.Perform(vote, true);
    }

    [When("the user votes {word} the answer {string}")]
    public void WhenTheUserVotesUpTheAnswer(VoteDirection vote, string answerContent)
    {
        questionDetailsPageDriver.LoadPage();
        var answer = questionDetailsPageDriver.GetAnswerByContentFromPageContent(answerContent);
        voteAnswerDriver.Perform(answer.Id, vote);
    }

    [Given("another user votes {word} the answer in the meanwhile")]
    public void GivenAnotherUserVotesUpTheAnswerInTheMeanwhile(VoteDirection vote)
    {
        var otherUserAuthContext = isolatedAuthContextFactory.CreateAuthContext();
        otherUserAuthContext.Authenticate(DomainDefaults.AltUserName, DomainDefaults.AltUserPassword);
        var otherUserVoteDriver = isolatedAuthContextFactory.CreateDriver<VoteAnswerDriver>(otherUserAuthContext);
        otherUserVoteDriver.Perform(questionContext.CurrentQuestionId, questionContext.CurrentAnswerId, vote);
    }

    [Then("the vote count of the answer should be changed to {int}")]
    public void ThenTheVoteCountOfTheAnswerShouldBeChangedTo(int expectedVoteCount)
    {
        questionContext.CurrentAnswer.Votes.Should().Be(expectedVoteCount);
    }

    [Then("the answer voting attempt should fail with error {string}")]
    public void ThenTheAnswerVotingAttemptShouldFailWithError(string expectedErrorMessageKey)
    {
        voteAnswerDriver.ShouldFailWithError(expectedErrorMessageKey);
    }
}