using System.Text.Json.Serialization;

namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini candidate DTO
/// </summary>
public class CandidateDto
{
    /// <summary>
    /// Gemini content
    /// </summary>
    [JsonPropertyName("content")]
    public ContentDto Content { get; set; }

    /// <summary>
    /// Gemini finishReason
    /// </summary>
    [JsonPropertyName("finishReason")]
    public string FinishReason { get; set; }

    /// <summary>
    /// Gemini avgLogprobs
    /// </summary>
    [JsonPropertyName("avgLogprobs")]
    public double AvgLogProbs { get; set; }
}