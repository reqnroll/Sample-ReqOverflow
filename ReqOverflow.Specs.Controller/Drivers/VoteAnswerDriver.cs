using System;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers;

public class VoteAnswerDriver(
    QuestionContext questionContext,
    AuthContext authContext,
    QuestionDetailsPageDriver questionDetailsPageDriver)
    : ActionAttempt<Tuple<Guid, Guid, VoteDirection>, AnswerDetailModel>(input =>
    {
        var controller = new QuestionController();
        var result = controller
            .VoteAnswer(input.Item1, input.Item2, (int)input.Item3, authContext.AuthToken);
        questionDetailsPageDriver.LoadPage(input.Item1);
        return result;
    })
{
    public AnswerDetailModel Perform(Guid questionId, Guid answerId, VoteDirection vote, bool attemptOnly = false) => 
        Perform(new Tuple<Guid, Guid, VoteDirection>(questionId, answerId, vote), attemptOnly);

    public AnswerDetailModel Perform(Guid answerId, VoteDirection vote, bool attemptOnly = false) => 
        Perform(questionContext.CurrentQuestionId, answerId, vote, attemptOnly);

    public AnswerDetailModel Perform(VoteDirection vote, bool attemptOnly = false) => 
        questionContext.CurrentAnswer = Perform(questionContext.CurrentQuestionId, questionContext.CurrentAnswerId, vote, attemptOnly);
}