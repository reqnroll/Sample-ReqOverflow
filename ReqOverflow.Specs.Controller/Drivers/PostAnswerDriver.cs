using System;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers
{
    public class PostAnswerDriver : ActionAttempt<AnswerInputModel, AnswerDetailModel>
    {
        public PostAnswerDriver(QuestionContext questionContext, AuthContext authContext, QuestionDetailsPageDriver questionDetailsPageDriver) : base(answerInput =>
        {
            var controller = new QuestionController();
            var result = questionContext.CurrentAnswer = controller
                .PostAnswer(questionContext.CurrentQuestionId, answerInput, authContext.AuthToken);
            questionDetailsPageDriver.LoadPage(questionContext.CurrentQuestionId);
            return result;
        })
        {
        }
    }
}
