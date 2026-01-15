using System;
using System.Linq;
using OpenQA.Selenium;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Drivers;

public class AskQuestionPageDriver(BrowserContext browserContext) : ActionAttempt<AskInputModel, QuestionSummaryModel>
{
    private const string PageUrl = "/Ask";
    private IWebElement TitleInput => browserContext.Driver.FindElement(By.Id("TitleInput"));
    private IWebElement BodyInput => browserContext.Driver.FindElement(By.Id("BodyInput"));
    private IWebElement TagsInput => browserContext.Driver.FindElement(By.Id("Tags"));
    private IWebElement PostQuestionButton => browserContext.Driver.FindElement(By.Id("PostQuestionButton"));

    public void GoTo()
    {
        browserContext.NavigateTo(PageUrl);
    }

    protected override QuestionSummaryModel DoAction(AskInputModel askInput)
    {
        GoTo();
        TitleInput.SendKeys(askInput.Title);
        BodyInput.SendKeys(askInput.Body);
        TagsInput.SendKeys(string.Join(",", askInput.Tags));
        browserContext.SubmitFormWith(PostQuestionButton, true);
        browserContext.AssertNotOnPath(PageUrl);
        Console.WriteLine(browserContext.Driver.Url);
        return new QuestionSummaryModel
        {
            Id = Guid.Parse(browserContext.Driver.Url.Split('=').Last()),
            Title = askInput.Title
        };
    }
}