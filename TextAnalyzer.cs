using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace TextAnalysis
{
    // Enum for sentiment classification
    public enum Sentiment
    {
        Positive,
        Negative,
        Neutral
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
            _aiService = aiService;
            _configs = configs;
        }

        public Sentiment AnalyzeComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));

            // Get the prompt from the configuration
            string prompt = _configs.GetSentimentPrompt(comment);

            // Call the AI service with the prompt
            string aiResponse = _aiService.AnalyzeText(prompt);

            // Analyze the AI response using regex
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
                throw new InvalidOperationException("AI response did not contain recognizable sentiment keywords.");
            }
        }
    }
