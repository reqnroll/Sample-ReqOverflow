using ReqOverflow.Specs.API.Drivers;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.API.StepDefinitions;

[Binding]
public class QuestionAskingStepDefinitions(QuestionApiDriver askQuestionDriver)
{
    private QuestionDetailModel _questionDetails;
    private Table _askQuestionSpecification;

    [When("the user asks a question as")]
    public void WhenTheUserAsksAQuestionAs(Table questionTable)
    {
        _askQuestionSpecification = questionTable;
        var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        var result = askQuestionDriver.AskQuestion.Perform(question);
        _questionDetails = askQuestionDriver.GetQuestionDetails(result.Id);
    }

    [When("the user attempts to ask a question")]
    public void WhenTheUserAttemptsToAskAQuestion()
    {
        askQuestionDriver.AskQuestion.Perform(DomainDefaults.GetDefaultAskInput(), true);
    }

    [When("the user attempts to ask a question as")]
    public void WhenTheUserAttemptsToAskAQuestionAs(Table questionTable)
    {
        var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        askQuestionDriver.AskQuestion.Perform(question, true);
    }

    [Then("the question should be posted as above")]
    public void ThenTheQuestionShouldBePostedAsAbove()
    {
        _askQuestionSpecification.CompareToInstance(_questionDetails.ToQuestionData());
    }

    [Then("the question meta data should be")]
    public void ThenTheQuestionMetaDataShouldBe(Table expectedQuestionMetaDataTable)
    {
        expectedQuestionMetaDataTable.CompareToInstance(_questionDetails.ToQuestionData());
    }

    [Then("the ask attempt should fail with error {string}")]
    public void ThenTheAskAttemptShouldFailWithError(string expectedErrorMessageKey)
    {
        askQuestionDriver.AskQuestion.ShouldFailWithError(expectedErrorMessageKey);
    }
}