using System.Linq;
using AwesomeAssertions;
using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionDetailsStepDefinitions(
    QuestionContext questionContext,
    QuestionDetailsPageDriver questionDetailsPageDriver)
{
    [Given("the user opens the question details page of the question")]
    [When("the user checks the question details page of the question")]
    public void WhenTheUserChecksTheQuestionDetailsPageOfTheQuestion()
    {
        questionDetailsPageDriver.LoadPage(questionContext.CurrentQuestionId);
    }

    [Then("the question details should be shown as")]
    public void ThenTheQuestionDetailsShouldBeShownAs(Table expectedQuestionDetailsTable)
    {
        expectedQuestionDetailsTable.CompareToInstance(questionDetailsPageDriver.PageContent.ToQuestionData());
    }

    [Then("the question details should be shown as above")]
    public void ThenTheQuestionDetailsShouldBeShownAsAbove()
    {
        questionContext.QuestionSpecification.CompareToInstance(questionDetailsPageDriver.PageContent.ToQuestionData());
    }

    [Then("the answer should be listed among the answers as above")]
    public void ThenTheAnswerShouldBeListedAmongTheAnswersAsAbove()
    {
        var answer = questionDetailsPageDriver.GetAnswerByIdFromPageContent();
        questionContext.AnswerSpecification.CompareToInstance(answer.ToAnswerData());
    }

    [Then("the answer should be listed among the answers as/with")]
    public void ThenTheAnswerShouldBeListedAmongTheAnswersAs(Table expectedAnswerTable)
    {
        var answer = questionDetailsPageDriver.GetAnswerByContentFromPageContent(questionContext.CurrentAnswer.Content);

        expectedAnswerTable.CompareToInstance(answer.ToAnswerData());
    }

    [Then("the answers list should contain {int} answers")]
    public void ThenTheAnswersListShouldContainAnswers(int expectedAnswerCount)
    {
        questionDetailsPageDriver.PageContent.Answers.Should().HaveCount(expectedAnswerCount);
    }

    [Then("the answer list should be shown as")]
    public void ThenTheAnswerListShouldBeShownAs(Table expectedAnswerListTable)
    {
        var actualAnswers = questionDetailsPageDriver.PageContent.Answers.Select(a => a.ToAnswerData());
        expectedAnswerListTable.CompareToSet(actualAnswers, true);
    }

    [Then("the answers list should be ordered descending by vote")]
    public void ThenTheAnswersListShouldBeOrderedDescendingByVote()
    {
        questionDetailsPageDriver.PageContent.Answers.Should().BeInDescendingOrder(a => a.Votes);
    }
}