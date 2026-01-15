using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionListStepDefinitions(QuestionContext questionContext)
{
    private List<QuestionSummaryModel> _questions;

    [When("the user checks the questions page")]
    public void WhenTheUserChecksTheQuestionsPage()
    {
        var controller = new QuestionController();
        _questions = controller.GetQuestions();
    }

    [Then("the question should be listed among the questions as above")]
    public void ThenTheQuestionShouldBeListedAmongTheQuestionsAsAbove()
    {
        var question = _questions.FirstOrDefault(q => q.Title == questionContext.CurrentQuestion.Title);
        question.Should().NotBeNull();
        questionContext.QuestionSpecification.CompareToInstance(question.ToQuestionData());
    }

    [Then("the questions list should contain {int} questions")]
    public void ThenTheQuestionsListShouldContainQuestions(int expectedCount)
    {
        _questions.Should().HaveCount(expectedCount);
    }

    [Then("the question list should be ordered descending by ask date")]
    public void ThenTheQuestionListShouldBeOrderedDescendingByAskDate()
    {
        _questions.Should().BeInDescendingOrder(q => q.AskedAt);
    }
}