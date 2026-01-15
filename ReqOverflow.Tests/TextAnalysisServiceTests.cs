using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqOverflow.Web.Services;

namespace ReqOverflow.Tests;

[TestClass]
public class TextAnalysisServiceTests
{
    [TestMethod]
    public void GetRelevantWords_should_get_words_from_one_string()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["two words"]);
            
        CollectionAssert.AreEqual(new[] { "two", "words"}, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_get_words_from_multiple_strings()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["two words", "other ones"]);
            
        CollectionAssert.AreEqual(new[] { "two", "words", "other", "ones"}, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_get_provide_distinct_results()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["two words two", "other two words"]);
            
        CollectionAssert.AreEqual(new[] { "two", "words", "other" }, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_split_by_non_word_parts()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["one two, three.four"]);

        CollectionAssert.AreEqual(new[] { "one", "two", "three", "four" }, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_provide_trimmed_lowercase_results()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["oNE", " TWO "]);

        CollectionAssert.AreEqual(new[] { "one", "two" }, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_ignore_null_or_whitespace_strings()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords([null, "  ", "", "two words"]);

        CollectionAssert.AreEqual(new[] { "two", "words" }, result);
    }

    [TestMethod]
    public void GetRelevantWords_should_ignore_common_words()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetRelevantWords(["two is words"]);

        CollectionAssert.AreEqual(new[] { "two", "words" }, result);
    }

    [TestMethod]
    public void GetSimilarityIndex_should_return_at_least_half_for_common_words()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetSimilarityIndex(["two words"], ["one", "two"]);

        Assert.IsGreaterThanOrEqualTo(result, 0.5, $"result was '{result}'");
    }

    [TestMethod]
    public void GetSimilarityIndex_should_return_less_than_half_for_no_common_words()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetSimilarityIndex(["other words"], ["one", "two"]);

        Assert.IsLessThan(0.5, result, $"result was '{result}'");
    }

    [TestMethod]
    public void GetSimilarityIndex_should_return_less_than_one_even_for_multiple_common_words()
    {
        var sut = new TextAnalysisService();

        var result = sut.GetSimilarityIndex(["one two"], ["one", "two"]);

        Assert.IsLessThanOrEqualTo(1, result, $"result was '{result}'");
    }

    [TestMethod]
    public void GetSimilarityIndex_result_does_not_depend_on_word_order()
    {
        var sut = new TextAnalysisService();

        var result1 = sut.GetSimilarityIndex(["one other two here"], ["one", "two"]);
        var result2 = sut.GetSimilarityIndex(["two one other here"], ["one", "two"]);

        Assert.AreEqual(result1, result2);
    }

    [TestMethod]
    public void GetSimilarityIndex_should_return_higher_result_for_more_common_words()
    {
        var sut = new TextAnalysisService();

        var result1 = sut.GetSimilarityIndex(["one other three here"], ["one", "two"]);
        var result2 = sut.GetSimilarityIndex(["two one other here"], ["one", "two"]);

        Assert.IsGreaterThan(result1, result2, $"single common word returned '{result1}', multiple returned '{result2}'");
    }
}