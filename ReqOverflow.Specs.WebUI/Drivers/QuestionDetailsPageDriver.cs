using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Drivers;

public class QuestionDetailsPageDriver(BrowserContext browserContext)
{
    private IWebElement QuestionDetails => browserContext.Driver.FindElement(By.Id("QuestionDetails"));
    private IWebElement QuestionTitle => browserContext.Driver.FindElement(By.Id("QuestionTitle"));
    private IWebElement QuestionBody => browserContext.Driver.FindElement(By.Id("QuestionBody"));
    private IWebElement QuestionVotes => browserContext.Driver.FindElement(By.Id("QuestionVotes"));
    private IWebElement QuestionStats => browserContext.Driver.FindElement(By.Id("QuestionStats"));
    private IWebElement QuestionViews => browserContext.Driver.FindElement(By.Id("QuestionViews"));
    private ReadOnlyCollection<IWebElement> QuestionTags => browserContext.Driver.FindElements(By.CssSelector("#QuestionTags .post-tag"));
    private ReadOnlyCollection<IWebElement> AnswerDivs => browserContext.Driver.FindElements(By.CssSelector("#Answers .answer-info"));

    public QuestionDetailModel GetQuestionDetails()
    {
        return new()
        {
            Id = Guid.Parse(QuestionDetails.GetAttribute("data-question-id") ?? "unknown"),
            Title = QuestionTitle.Text,
            Body = QuestionBody.Text,
            Tags = QuestionTags.Select(te => te.Text).ToList(),
            Votes = int.Parse(QuestionVotes.Text),
            AskedBy = new UserReferenceModel { Name = QuestionStats.FindElement(By.CssSelector(".user-name")).Text },
            AskedAt = DateTime.Parse(QuestionStats.FindElement(By.CssSelector(".timestamp")).GetAttribute("data-time") ?? "unknown"),
            Views = int.Parse(QuestionViews.Text),
            Answers = ParseAnswers().ToList()
        };
    }


    private IEnumerable<AnswerDetailModel> ParseAnswers()
    {
        foreach (var answerDiv in AnswerDivs.ToArray())
        {
            yield return new AnswerDetailModel
            {
                Id = Guid.Parse(answerDiv.GetAttribute("data-answer-id") ?? "unknown"),
                Content = answerDiv.FindElement(By.CssSelector(".post-cell .multi-line")).Text,
                Votes = int.Parse(answerDiv.FindElement(By.CssSelector(".current-votes")).Text),
                AnsweredBy = new UserReferenceModel { Name = answerDiv.FindElement(By.CssSelector(".answer-stats .user-name")).Text },
                AnsweredAt = DateTime.Parse(answerDiv.FindElement(By.CssSelector(".answer-stats .timestamp")).GetAttribute("data-time") ?? "unknown")
            };
        }
    }

}