// This file is a copy of ReqOverflow.Specs.Controller/StepDefinitions/QuestionDataStepDefinitions.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using System.Linq;
using ReqOverflow.Specs.Drivers;
using ReqOverflow.Specs.Support;
using Reqnroll;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.StepDefinitions;

[Binding]
public class QuestionDataStepDefinitions(QuestionMother questionMother, QuestionContext questionContext)
{
    [Given("there is a question asked")]
    public void GivenThereIsAQuestionAsked()
    {
        questionMother.GenerateQuestions([DomainDefaults.GetDefaultQuestion()]);
    }

    [Given("there are questions asked as")]
    [Given("there is a question (just )asked as")]
    public void GivenThereIsAQuestionAskedAs(Table questionsTable)
    {
        var questions = questionsTable.CreateSet(() => DomainDefaults.GetDefaultQuestion());
        questionMother.GenerateQuestions(questions);

        questionContext.QuestionSpecification = questionsTable;
    }

    [Given("there is a question asked with {int} votes")]
    public void GivenThereIsAQuestionAskedWithVotes(int votes)
    {
        questionMother.GenerateQuestions([
            DomainDefaults.GetDefaultQuestion(question =>
            {
                question.Votes = votes;
            })
        ]);
    }

    [Given("there is a question asked by {word}")]
    public void GivenThereIsAQuestionAskedByUser(string userName)
    {
        questionMother.GenerateQuestions([
            DomainDefaults.GetDefaultQuestion(question =>
            {
                question.AskedBy = userName;
            })
        ]);
    }


    [Given("there are {int} questions asked")]
    public void GivenThereAreQuestionsAsked(int count)
    {
        questionMother.GenerateQuestions(Enumerable.Range(0, count)
            .Select(_ => DomainDefaults.GetDefaultQuestion()));
    }

    [Given("there are answers for the question as")]
    [Given("there is an answer for the question as")]
    public void GivenThereIsAnAnswerForTheQuestionAs(Table answersTable)
    {
        var answers = answersTable.CreateSet(() => DomainDefaults.GetDefaultAnswer());
        questionMother.GenerateAnswersForCurrentQuestion(answers);

        questionContext.AnswerSpecification = answersTable;
    }

    [Given("there is an answer for the question")]
    public void GivenThereIsAnAnswerForTheQuestion()
    {
        questionMother.GenerateAnswersForCurrentQuestion([DomainDefaults.GetDefaultAnswer()]);
    }

    [Given("there is an answer for the question by {word}")]
    public void GivenThereIsAnAnswerForTheQuestionByUser(string userName)
    {
        questionMother.GenerateAnswersForCurrentQuestion([
            DomainDefaults.GetDefaultAnswer(answer =>
            {
                answer.AnsweredBy = userName;
            })
        ]);
    }
}