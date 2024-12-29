using System;
using Xunit;

namespace SocialMediaAnalysis.Tests
{
    public class TextAnalyzerTests
    {
        // Mock implementation of IAIService
        private class MockAIService : IAIService
        {
            private readonly string _response;

            public MockAIService(string response)
            {
                _response = response;
            }

            public string GetResponse(string prompt)
            {
                return _response;
            }
        }

        // Mock implementation of ITextAnalyzerConfigs
        private class MockTextAnalyzerConfigs : ITextAnalyzerConfigs
        {
            public string SentimentAnalysisPrompt => "Analyze the sentiment of this comment: \"{0}\"";
        }

        [Fact]
        public void AnalyzeSentiment_PositiveComment_ReturnsPositive()
        {
            // Arrange
            var aiService = new MockAIService("The sentiment is positive.");
            var configs = new MockTextAnalyzerConfigs();
            var analyzer = new TextAnalyzer(aiService, configs);

            // Act
            var result = analyzer.AnalyzeSentiment("I love this product!");

            // Assert
            Assert.Equal(Sentiment.Positive, result);
        }

        [Fact]
        public void AnalyzeSentiment_NegativeComment_ReturnsNegative()
        {
            // Arrange
            var aiService = new MockAIService("The sentiment is negative.");
            var configs = new MockTextAnalyzerConfigs();
            var analyzer = new TextAnalyzer(aiService, configs);

            // Act
            var result = analyzer.AnalyzeSentiment("I hate this product!");

            // Assert
            Assert.Equal(Sentiment.Negative, result);
        }

        [Fact]
        public void AnalyzeSentiment_NeutralComment_ReturnsNeutral()
        {
            // Arrange
            var aiService = new MockAIService("The sentiment is neutral.");
            var configs = new MockTextAnalyzerConfigs();
            var analyzer = new TextAnalyzer(aiService, configs);

            // Act
            var result = analyzer.AnalyzeSentiment("This product is okay.");

            // Assert
            Assert.Equal(Sentiment.Neutral, result);
        }

        [Fact]
        public void AnalyzeSentiment_EmptyComment_ThrowsArgumentException()
        {
            // Arrange
            var aiService = new MockAIService("The sentiment is positive.");
            var configs = new MockTextAnalyzerConfigs();
            var analyzer = new TextAnalyzer(aiService, configs);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => analyzer.AnalyzeSentiment(string.Empty));
        }

        [Fact]
        public void AnalyzeSentiment_InvalidAIResponse_ThrowsInvalidOperationException()
        {
            // Arrange
            var aiService = new MockAIService("Unrecognized response.");
            var configs = new MockTextAnalyzerConfigs();
            var analyzer = new TextAnalyzer(aiService, configs);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => analyzer.AnalyzeSentiment("What is this product?"));
        }
    }
}
