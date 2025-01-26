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
    public required ContentDto Content { get; set; }

    /// <summary>
    /// Gemini finishReason
    /// </summary>
    [JsonPropertyName("finishReason")]
    public required string FinishReason { get; set; }

    /// <summary>
    /// Gemini avgLogprobs
    /// </summary>
    [JsonPropertyName("avgLogprobs")]
    public required double AvgLogProbs { get; set; }
}