// This file is a copy of ReqOverflow.Specs.Controller/Support/Data/AnswerData.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using System;
using ReqOverflow.Web.Models;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Support.Data;

public class AnswerData
{
    public string Content { get; set; }
    public int Votes { get; set; }

    public DateTime AnsweredAt { get; set; }
    public string AnsweredBy { get; set; }
}

internal static class AnswerDataExtensions
{
    public static AnswerData ToAnswerData(this AnswerDetailModel answerModel)
    {
        return new()
        {
            Content = answerModel.Content,
            Votes = answerModel.Votes,
            AnsweredAt = answerModel.AnsweredAt,
            AnsweredBy = answerModel.AnsweredBy.Name
        };
    }
}