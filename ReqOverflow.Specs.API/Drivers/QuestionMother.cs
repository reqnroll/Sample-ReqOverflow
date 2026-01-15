// This file is a copy of ReqOverflow.Specs.Controller/Drivers/QuestionMother.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using System.Collections.Generic;
using System.Linq;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.Support.Data;
using ReqOverflow.Web.DataAccess;
using ReqOverflow.Web.Services;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Drivers;

public class QuestionMother(
    QuestionContext questionContext,
    ModelTransformationService modelTransformationService,
    DataContext testDataContext,
    TestLogger testLogger)
{
    private Question CreateQuestion(QuestionData questionData)
    {
        testDataContext.EnsureTags(questionData.TagLabels);
            
        return new()
        {
            Title = questionData.Title,
            Body = questionData.Body,
            TagIds = testDataContext.GetTagIds(questionData.TagLabels),
            Views = questionData.Views,
            Votes = questionData.Votes,
            AskedAt = questionData.AskedAt,
            AskedBy = testDataContext.FindUserByName(questionData.AskedBy).Id
        };
    }

    private Answer CreateAnswer(AnswerData ad)
    {
        return new()
        {
            Content = ad.Content,
            Votes = ad.Votes,
            AnsweredAt = ad.AnsweredAt,
            AnsweredBy = testDataContext.FindUserByName(ad.AnsweredBy).Id,
        };
    }

    public void GenerateQuestions(IEnumerable<QuestionData> questions)
    {
        foreach (var questionData in questions)
        {
            var question = CreateQuestion(questionData);
            GenerateAnswers(question, questionData.Answers);
            testDataContext.Questions.Add(question);
            var questionModel = modelTransformationService.ToQuestionDetails(question);
            questionContext.QuestionsCreated.Add(questionModel);
            questionContext.CurrentQuestion = questionModel;

            testLogger.LogCreatedQuestion(questionModel);
        }
        testDataContext.SaveChanges();
    }

    private void GenerateAnswers(Question question, in int answerCount)
    {
        var answers = Enumerable.Range(0, answerCount)
            .Select(_ => DomainDefaults.GetDefaultAnswer());
        GenerateAnswers(question, answers);
    }

    private void GenerateAnswers(Question question, IEnumerable<AnswerData> answersData)
    {
        var answers = answersData
            .Select(CreateAnswer)
            .ToList();
        question.Answers = answers;

        if (answers.Any())
            questionContext.CurrentAnswer = modelTransformationService.ToAnswerDetails(answers.Last());
    }

    public void GenerateAnswersForCurrentQuestion(IEnumerable<AnswerData> answers)
    {
        var question = testDataContext.GetQuestionById(questionContext.CurrentQuestionId);
        GenerateAnswers(question, answers);
        testDataContext.SaveChanges();
    }
}