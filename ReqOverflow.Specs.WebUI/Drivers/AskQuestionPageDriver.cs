using System;
using System.Linq;
using OpenQA.Selenium;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Drivers
{
    public class AskQuestionPageDriver : ActionAttempt<AskInputModel, QuestionSummaryModel>
    {
        private readonly BrowserContext _browserContext;

        private const string PageUrl = "/Ask";
        private IWebElement TitleInput => _browserContext.Driver.FindElement(By.Id("TitleInput"));
        private IWebElement BodyInput => _browserContext.Driver.FindElement(By.Id("BodyInput"));
        private IWebElement TagsInput => _browserContext.Driver.FindElement(By.Id("Tags"));
        private IWebElement PostQuestionButton => _browserContext.Driver.FindElement(By.Id("PostQuestionButton"));

        public AskQuestionPageDriver(BrowserContext browserContext)
        {
            _browserContext = browserContext;
        }

        public void GoTo()
        {
            _browserContext.NavigateTo(PageUrl);
        }

        protected override QuestionSummaryModel DoAction(AskInputModel askInput)
        {
            GoTo();
            TitleInput.SendKeys(askInput.Title);
            BodyInput.SendKeys(askInput.Body);
            TagsInput.SendKeys(string.Join(",", askInput.Tags));
            _browserContext.SubmitFormWith(PostQuestionButton, true);
            _browserContext.AssertNotOnPath(PageUrl);
            Console.WriteLine(_browserContext.Driver.Url);
            return new QuestionSummaryModel
            {
                Id = Guid.Parse(_browserContext.Driver.Url.Split('=').Last()),
                Title = askInput.Title
            };
        }
    }
}
