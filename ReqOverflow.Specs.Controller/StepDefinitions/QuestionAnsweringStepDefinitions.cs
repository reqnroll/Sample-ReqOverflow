using ReqOverflow.Specs.Controller.Drivers;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionAnsweringStepDefinitions(PostAnswerDriver postAnswer)
{
    [When("the user answers the question as")]
    public void WhenTheUserAnswersTheQuestionAs(Table answerTable)
    {
        var answer = answerTable.CreateInstance<AnswerInputModel>();
        postAnswer.Perform(answer);
    }

    [When("the user attempts to answer the question")]
    public void WhenTheUserAttemptsToAnswerTheQuestion()
    {
        postAnswer.Perform(new AnswerInputModel {Content = "Sample content"}, true);
    }

    [When("the user attempts to answer the question as")]
    public void WhenTheUserAttemptsToAnswerTheQuestionAs(Table answerTable)
    {
        var answer = answerTable.CreateInstance<AnswerInputModel>();
        postAnswer.Perform(answer, true);
    }

    [Then("the answer attempt should fail with error {string}")]
    public void ThenTheAnswerAttemptShouldFailWithError(string expectedErrorMessageKey)
    {
        postAnswer.ShouldFailWithError(expectedErrorMessageKey);
    }
}