using NUnit.Framework;
using MyProject.TextAnalysis; // Namespace for your main project classes

[TestFixture]
public class TextAnalyzerTests
{
    private class MockAIService : IAIService
    {
        private readonly string _response;

        public MockAIService(string response)
        {
            _response = response;
        }

        public string AnalyzeText(string prompt)
        {
            return _response;
        }
    }

    private class MockTextAnalyzerConfigs : ITextAnalyzerConfigs
    {
        public string GetSentimentPrompt(string comment)
        {
            return $"Classify the following comment as positive, negative, or neutral: '{comment}'";
        }
    }

    [Test]
    public void AnalyzeComment_PositiveComment_ReturnsPositive()
    {
        var aiService = new MockAIService("The sentiment is positive.");
        var configs = new MockTextAnalyzerConfigs();
        var analyzer = new TextAnalyzer(aiService, configs);

        var result = analyzer.AnalyzeComment("I love this product!");

        Assert.AreEqual(Sentiment.Positive, result);
    }

    [Test]
    public void AnalyzeComment_NegativeComment_ReturnsNegative()
    {
        var aiService = new MockAIService("The sentiment is negative.");
        var configs = new MockTextAnalyzerConfigs();
        var analyzer = new TextAnalyzer(aiService, configs);

        var result = analyzer.AnalyzeComment("I hate this product!");

        Assert.AreEqual(Sentiment.Negative, result);
    }

    [Test]
    public void AnalyzeComment_NeutralComment_ReturnsNeutral()
    {
        var aiService = new MockAIService("The sentiment is neutral.");
        var configs = new MockTextAnalyzerConfigs();
        var analyzer = new TextAnalyzer(aiService, configs);

        var result = analyzer.AnalyzeComment("This product is okay.");

        Assert.AreEqual(Sentiment.Neutral, result);
    }

    [Test]
    public void AnalyzeComment_EmptyComment_ThrowsArgumentException()
    {
        var aiService = new MockAIService("The sentiment is positive.");
        var configs = new MockTextAnalyzerConfigs();
        var analyzer = new TextAnalyzer(aiService, configs);

        Assert.Throws<ArgumentException>(() => analyzer.AnalyzeComment(string.Empty));
    }

    [Test]
    public void AnalyzeComment_InvalidAIResponse_ThrowsInvalidOperationException()
    {
        var aiService = new MockAIService("Unrecognized response.");
        var configs = new MockTextAnalyzerConfigs();
        var analyzer = new TextAnalyzer(aiService, configs);

        Assert.Throws<InvalidOperationException>(() => analyzer.AnalyzeComment("What is this product?"));
    }
}
}