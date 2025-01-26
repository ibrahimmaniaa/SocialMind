using System.Text.Json.Serialization;


namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini usageMetadata DTO
/// </summary>
public class UsageMetadataDto
{
    /// <summary>
    /// Gemini promptTokenCount
    /// </summary>
    [JsonPropertyName("promptTokenCount")]
    public long PromptTokenCount { get; set; }

    /// <summary>
    /// Gemini candidatesTokenCount
    /// </summary>
    [JsonPropertyName("candidatesTokenCount")]
    public long CandidatesTokenCount { get; set; }

    /// <summary>
    /// Gemini totalTokenCount
    /// </summary>
    [JsonPropertyName("totalTokenCount")]
    public long TotalTokenCount { get; set; }
}