using System.Collections.Generic;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.StepDefinitions;

[Binding]
public class QuestionSuggestionsStepDefinitions
{
    private List<QuestionSummaryModel> _suggestions;

    [When("the user starts asking a question as")]
    public void WhenTheUserStartsAskingAQuestionAs(Table questionTable)
    {
        var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        var controller = new QuestionSuggestionsController();
        _suggestions = controller.GetQuestionSuggestions(question);
    }

    [Then("the suggestions list should be")]
    public void ThenTheSuggestionsListShouldBe(Table expectedSuggestionsTable)
    {
        expectedSuggestionsTable.CompareToSet(_suggestions);
    }

    [Then("the suggestions list should be provided in this order")]
    public void ThenTheSuggestionsListShouldBeProvidedInThisOrder(Table expectedSuggestionsTable)
    {
        expectedSuggestionsTable.CompareToSet(_suggestions, true);
    }
}