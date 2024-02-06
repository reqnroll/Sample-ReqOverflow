using System;
using System.Linq;
using ReqOverflow.Specs.Drivers;
using ReqOverflow.Specs.Support;
using Reqnroll;
using Reqnroll.Assist;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.StepDefinitions
{
    [Binding]
    public class QuestionDataStepDefinitions
    {
        private readonly QuestionMother _questionMother;
        private readonly QuestionContext _questionContext;

        public QuestionDataStepDefinitions(QuestionMother questionMother, QuestionContext questionContext)
        {
            _questionMother = questionMother;
            _questionContext = questionContext;
        }

        [Given("there is a question asked")]
        public void GivenThereIsAQuestionAsked()
        {
            _questionMother.GenerateQuestions(new [] { DomainDefaults.GetDefaultQuestion() });
        }

        [Given("there are questions asked as")]
        [Given("there is a question (just )asked as")]
        public void GivenThereIsAQuestionAskedAs(Table questionsTable)
        {
            var questions = questionsTable.CreateSet(() => DomainDefaults.GetDefaultQuestion());
            _questionMother.GenerateQuestions(questions);

            _questionContext.QuestionSpecification = questionsTable;
        }

        [Given("there is a question asked with {int} votes")]
        public void GivenThereIsAQuestionAskedWithVotes(int votes)
        {
            _questionMother.GenerateQuestions(new[]
            {
                DomainDefaults.GetDefaultQuestion(question =>
                {
                    question.Votes = votes;
                })
            });
        }

        [Given("there is a question asked by {word}")]
        public void GivenThereIsAQuestionAskedByUser(string userName)
        {
            _questionMother.GenerateQuestions(new[]
            {
                DomainDefaults.GetDefaultQuestion(question =>
                {
                    question.AskedBy = userName;
                })
            });
        }


        [Given("there are {int} questions asked")]
        public void GivenThereAreQuestionsAsked(int count)
        {
            _questionMother.GenerateQuestions(Enumerable.Range(0, count)
                .Select(_ => DomainDefaults.GetDefaultQuestion()));
        }

        [Given("there are answers for the question as")]
        [Given("there is an answer for the question as")]
        public void GivenThereIsAnAnswerForTheQuestionAs(Table answersTable)
        {
            var answers = answersTable.CreateSet(() => DomainDefaults.GetDefaultAnswer());
            _questionMother.GenerateAnswersForCurrentQuestion(answers);

            _questionContext.AnswerSpecification = answersTable;
        }

        [Given("there is an answer for the question")]
        public void GivenThereIsAnAnswerForTheQuestion()
        {
            _questionMother.GenerateAnswersForCurrentQuestion(new []{ DomainDefaults.GetDefaultAnswer() });
        }

        [Given("there is an answer for the question by {word}")]
        public void GivenThereIsAnAnswerForTheQuestionByUser(string userName)
        {
            _questionMother.GenerateAnswersForCurrentQuestion(new[]
            {
                DomainDefaults.GetDefaultAnswer(answer =>
                {
                    answer.AnsweredBy = userName;
                })
            });
        }
    }
}
