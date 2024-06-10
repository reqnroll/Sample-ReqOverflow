// This file is a copy of ReqOverflow.Specs.Controller/Support/Data/QuestionData.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using System;
using System.Collections.Generic;
using ReqOverflow.Web.Models;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Support.Data
{
    public class QuestionData
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int Answers { get; set; }
        public DateTime AskedAt { get; set; }
        public string AskedBy { get; set; }

        public IEnumerable<string> TagLabels =>
            Tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
    }

    internal static class QuestionDataExtensions
    {
        public static QuestionData ToQuestionData(this QuestionSummaryModel questionModel)
        {
            return new()
            {
                Title = questionModel.Title,
                Votes = questionModel.Votes,
                Views = questionModel.Views,
                AskedAt = questionModel.AskedAt,
                AskedBy = questionModel.AskedBy.Name,
                Answers = questionModel.Answers
            };
        }

        public static QuestionData ToQuestionData(this QuestionDetailModel questionModel)
        {
            return new()
            {
                Title = questionModel.Title,
                Body = questionModel.Body,
                Tags = string.Join(",", questionModel.Tags),
                Votes = questionModel.Votes,
                Views = questionModel.Views,
                Answers = questionModel.Answers.Count,
                AskedAt = questionModel.AskedAt,
                AskedBy = questionModel.AskedBy.Name
            };
        }
    }
}