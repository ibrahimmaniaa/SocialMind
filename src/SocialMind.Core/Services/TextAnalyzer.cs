using System;
using System.Text.RegularExpressions;
using SocialMind.Core.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMind.Core.Services
{
    /// <summary>
    /// Exception thrown when the AI response does not contain recognizable sentiment keywords.
    /// </summary>
    public class UnrecognizedSentimentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnrecognizedSentimentException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public UnrecognizedSentimentException(string message) : base(message) { }
    }

    /// <summary>
    /// Interface representing an AI service that analyzes text.
    /// </summary>
    public interface IAIService
    {
        /// <summary>
        /// Sends a prompt to the AI service and returns its response.
        /// </summary>
        /// <param name="prompt">The input prompt to be analyzed.</param>
        /// <returns>The AI-generated response.</returns>
        string AnalyzeText(string prompt);
    }

    /// <summary>
    /// Interface for configuration settings related to text analysis.
    /// </summary>
    public interface ITextAnalyzerConfigs
    {
        /// <summary>
        /// Generates a sentiment analysis prompt for a given comment.
        /// </summary>
        /// <param name="comment">The comment to analyze.</param>
        /// <returns>A formatted prompt for sentiment analysis.</returns>
        string GetSentimentPrompt(string comment);
    }

    /// <summary>
    /// Provides sentiment analysis for social media comments using an AI service.
    /// </summary>
    public class TextAnalyzer
    {
        private readonly IAIService _aiService;
        private readonly ITextAnalyzerConfigs _configs;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAnalyzer"/> class.
        /// </summary>
        /// <param name="aiService">The AI service used for text analysis.</param>
        /// <param name="configs">Configuration settings for the text analyzer.</param>
        /// <exception cref="ArgumentNullException">Thrown if any dependency is null.</exception>
        public TextAnalyzer(IAIService aiService, ITextAnalyzerConfigs configs)
        {
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }

        /// <summary>
        /// Analyzes a social media comment and classifies its sentiment.
        /// </summary>
        /// <param name="comment">The comment to analyze.</param>
        /// <returns>The detected sentiment: <see cref="Sentiment.Positive"/>, <see cref="Sentiment.Negative"/>, or <see cref="Sentiment.Neutral"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the comment is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there is an issue communicating with the AI service.</exception>
        /// <exception cref="UnrecognizedSentimentException">Thrown when the AI response does not contain recognizable sentiment keywords.</exception>
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

            // Analyze the AI response using regex patterns
            var positivePattern = new Regex(@"\b(positive|good|excellent|happy|joy|great|optimistic)\b", RegexOptions.IgnoreCase);
            var negativePattern = new Regex(@"\b(negative|bad|poor|sad|angry|terrible|pessimistic)\b", RegexOptions.IgnoreCase);
            var neutralPattern = new Regex(@"\b(neutral|balanced|indifferent|middle ground)\b", RegexOptions.IgnoreCase);

            if (positivePattern.IsMatch(aiResponse))
            {
                return Sentiment.Positive;
            }

            if (negativePattern.IsMatch(aiResponse))
            {
                return Sentiment.Negative;
            }

            if (neutralPattern.IsMatch(aiResponse))
            {
                return Sentiment.Neutral;
            }

            throw new UnrecognizedSentimentException("AI response did not contain recognizable sentiment keywords.");
        }
    }
}