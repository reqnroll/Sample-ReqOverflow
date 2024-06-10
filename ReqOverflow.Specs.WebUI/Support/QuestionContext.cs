// This file is a copy of ReqOverflow.Specs.Controller/Support/QuestionContext.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using System;
using System.Collections.Generic;
using ReqOverflow.Web.Models;
using Reqnroll;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Support
{
    public class QuestionContext
    {
        public Table QuestionSpecification { get; set; }
        public Table AnswerSpecification { get; set; }

        public List<QuestionDetailModel> QuestionsCreated { get; } = new();

        public QuestionDetailModel CurrentQuestion { get; set; }
        public AnswerDetailModel CurrentAnswer { get; set; }

        public Guid CurrentQuestionId => CurrentQuestion.Id;
        public Guid CurrentAnswerId => CurrentAnswer.Id;
    }
}
