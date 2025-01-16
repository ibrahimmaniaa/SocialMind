using System;
using System.Text.RegularExpressions;

namespace SocialMind.Core.Services
{
    // Enum for sentiment classification
    public enum Sentiment
    {
        Positive,
        Negative,
        Neutral
    }

    // Custom exception for unrecognized sentiment
    public class UnrecognizedSentimentException : Exception
    {
        public UnrecognizedSentimentException(string message) : base(message) { }
    }

    // Interface for AI service
    public interface IAIService
    {
        string AnalyzeText(string prompt);
    }

    // Interface for TextAnalyzer configurations
    public interface ITextAnalyzerConfigs
    {
        string GetSentimentPrompt(string comment);
    }

    // TextAnalyzer class
    public class TextAnalyzer
    {
        private readonly IAIService _aiService;
        private readonly ITextAnalyzerConfigs _configs;

        public TextAnalyzer(IAIService aiService, ITextAnalyzerConfigs configs)
        {
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }

        public Sentiment AnalyzeComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));

            // Get the prompt from the configuration
            string prompt = _configs.GetSentimentPrompt(comment);

            // Call the AI service with the prompt
            string aiResponse;
            try
            {
                aiResponse = _aiService.AnalyzeText(prompt);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while communicating with the AI service.", ex);
            }

            if (string.IsNullOrWhiteSpace(aiResponse))
                throw new UnrecognizedSentimentException("AI response was empty or null.");

            // Analyze the AI response
            if (Regex.IsMatch(aiResponse, "\\bpositive\\b", RegexOptions.IgnoreCase))
            {
                return Sentiment.Positive;
            }
            else if (Regex.IsMatch(aiResponse, "\\bnegative\\b", RegexOptions.IgnoreCase))
            {
                return Sentiment.Negative;
            }
            else if (Regex.IsMatch(aiResponse, "\\bneutral\\b", RegexOptions.IgnoreCase))
            {
                return Sentiment.Neutral;
            }
            else
            {
                throw new UnrecognizedSentimentException("AI response did not contain recognizable sentiment keywords.");
            }
        }
    }
}
