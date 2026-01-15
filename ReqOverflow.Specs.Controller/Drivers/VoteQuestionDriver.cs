using System;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers;

public class VoteQuestionDriver(
    QuestionContext questionContext,
    AuthContext authContext,
    QuestionDetailsPageDriver questionDetailsPageDriver)
    : ActionAttempt<Tuple<Guid, VoteDirection>, QuestionSummaryModel>(input =>
    {
        var controller = new QuestionController();
        var result = controller
            .VoteQuestion(input.Item1, (int)input.Item2, authContext.AuthToken);
        questionDetailsPageDriver.LoadPage(input.Item1);
        return result;
    })
{
    public QuestionSummaryModel Perform(Guid questionId, VoteDirection vote, bool attemptOnly = false) => 
        Perform(new Tuple<Guid, VoteDirection>(questionId, vote), attemptOnly);

    public QuestionSummaryModel Perform(VoteDirection vote, bool attemptOnly = false) => 
        Perform(questionContext.CurrentQuestionId, vote, attemptOnly);
}