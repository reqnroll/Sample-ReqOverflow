using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionAskingStepDefinitions(
    AskQuestionDriver askQuestionDriver,
    QuestionDetailsPageDriver questionDetailsPageDriver)
{
    private Table _askQuestionSpecification;

    [When("the user asks a question as")]
    public void WhenTheUserAsksAQuestionAs(Table questionTable)
    {
        _askQuestionSpecification = questionTable;
        var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        var result = askQuestionDriver.Perform(question);
        questionDetailsPageDriver.LoadPage(result.Id);
    }

    [When("the user attempts to ask a question")]
    public void WhenTheUserAttemptsToAskAQuestion()
    {
        askQuestionDriver.Perform(DomainDefaults.GetDefaultAskInput(), true);
    }

    [When("the user attempts to ask a question as")]
    public void WhenTheUserAttemptsToAskAQuestionAs(AskInputModel askedQuestion)
    {
        askQuestionDriver.Perform(askedQuestion, true);
    }

    [Then("the question should be posted as above")]
    public void ThenTheQuestionShouldBePostedAsAbove()
    {
        _askQuestionSpecification.CompareToInstance(questionDetailsPageDriver.PageContent.ToQuestionData());
    }

    [Then("the question meta data should be")]
    public void ThenTheQuestionMetaDataShouldBe(Table expectedQuestionMetaDataTable)
    {
        expectedQuestionMetaDataTable.CompareToInstance(questionDetailsPageDriver.PageContent.ToQuestionData());
    }

    [Then("the ask attempt should fail with error {string}")]
    public void ThenTheAskAttemptShouldFailWithError(string expectedErrorMessageKey)
    {
        askQuestionDriver.ShouldFailWithError(expectedErrorMessageKey);
    }
}