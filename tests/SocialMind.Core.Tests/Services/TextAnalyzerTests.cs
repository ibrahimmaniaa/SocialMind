using System;
using Moq;
using Xunit;
using SocialMind.Core;
using SocialMind.Core.Services;


namespace SocialMind.Core.Tests.Services
{

    namespace SocialMind.Core.Services.Tests
    {
        public class TextAnalyzerTests
        {
            private readonly Mock<IAIService> _mockAIService;

            private readonly Mock<ITextAnalyzerConfigs> _mockConfigs;

            private readonly TextAnalyzer _textAnalyzer;


            public TextAnalyzerTests()
            {
                _mockAIService = new Mock<IAIService>();
                _mockConfigs = new Mock<ITextAnalyzerConfigs>();
                _textAnalyzer = new TextAnalyzer(_mockAIService.Object, _mockConfigs.Object);
            }


            [Fact]
            public void Constructor_NullAIService_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => new TextAnalyzer(null, _mockConfigs.Object));
            }


            [Fact]
            public void Constructor_NullConfigs_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => new TextAnalyzer(_mockAIService.Object, null));
            }


            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void AnalyzeComment_NullOrEmptyComment_ThrowsArgumentException(string comment)
            {
                Assert.Throws<ArgumentException>(() => _textAnalyzer.AnalyzeComment(comment));
            }


            [Fact]
            public void AnalyzeComment_ValidComment_ReturnsPositiveSentiment()
            {
                // Arrange
                string comment = "This is amazing!";
                string prompt = "Analyze sentiment: This is amazing!";
                string aiResponse = "The sentiment is positive.";

                _mockConfigs.Setup(c => c.GetSentimentPrompt(comment)).Returns(prompt);
                _mockAIService.Setup(s => s.AnalyzeText(prompt)).Returns(aiResponse);

                // Act
                var result = _textAnalyzer.AnalyzeComment(comment);

                // Assert
                Assert.Equal(Sentiment.Positive, result);
            }


            [Fact]
            public void AnalyzeComment_ValidComment_ReturnsNegativeSentiment()
            {
                // Arrange
                string comment = "This is terrible!";
                string prompt = "Analyze sentiment: This is terrible!";
                string aiResponse = "The sentiment is negative.";

                _mockConfigs.Setup(c => c.GetSentimentPrompt(comment)).Returns(prompt);
                _mockAIService.Setup(s => s.AnalyzeText(prompt)).Returns(aiResponse);

                // Act
                var result = _textAnalyzer.AnalyzeComment(comment);

                // Assert
                Assert.Equal(Sentiment.Negative, result);
            }


            [Fact]
            public void AnalyzeComment_ValidComment_ReturnsNeutralSentiment()
            {
                // Arrange
                string comment = "It's okay.";
                string prompt = "Analyze sentiment: It's okay.";
                string aiResponse = "The sentiment is neutral.";

                _mockConfigs.Setup(c => c.GetSentimentPrompt(comment)).Returns(prompt);
                _mockAIService.Setup(s => s.AnalyzeText(prompt)).Returns(aiResponse);

                // Act
                var result = _textAnalyzer.AnalyzeComment(comment);

                // Assert
                Assert.Equal(Sentiment.Neutral, result);
            }


            [Fact]
            public void AnalyzeComment_UnrecognizedAIResponse_ThrowsUnrecognizedSentimentException()
            {
                // Arrange
                string comment = "Confusing response.";
                string prompt = "Analyze sentiment: Confusing response.";
                string aiResponse = "The sentiment is ambiguous.";

                _mockConfigs.Setup(c => c.GetSentimentPrompt(comment)).Returns(prompt);
                _mockAIService.Setup(s => s.AnalyzeText(prompt)).Returns(aiResponse);

                // Act & Assert
                Assert.Throws<UnrecognizedSentimentException>(() => _textAnalyzer.AnalyzeComment(comment));
            }


            [Fact]
            public void AnalyzeComment_AIServiceThrowsException_ThrowsInvalidOperationException()
            {
                // Arrange
                string comment = "This is bad!";
                string prompt = "Analyze sentiment: This is bad!";

                _mockConfigs.Setup(c => c.GetSentimentPrompt(comment)).Returns(prompt);
                _mockAIService.Setup(s => s.AnalyzeText(prompt)).Throws(new Exception("Service error"));

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => _textAnalyzer.AnalyzeComment(comment));
            }
        }
    }
}
