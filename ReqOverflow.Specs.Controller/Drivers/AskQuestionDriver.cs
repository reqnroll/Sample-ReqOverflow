using System;
using ReqOverflow.Specs.Controller.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers
{
    public class AskQuestionDriver : ActionAttempt<AskInputModel, QuestionSummaryModel>
    {
        public AskQuestionDriver(AuthContext authContext) : base(askInput =>
        {
            var controller = new QuestionController();
            return controller.AskQuestion(askInput, authContext.AuthToken);
        })
        {
        }
    }
}
