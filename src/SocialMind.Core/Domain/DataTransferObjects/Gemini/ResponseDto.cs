using System.Text.Json.Serialization;


namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini response DTO
/// </summary>
public class ResponseDto : ResponseDtoBase
{
    /// <summary>
    /// Gemini candidates
    /// </summary>
    [JsonPropertyName("candidates")]
    public required CandidateDto[] Candidates { get; set; }

    /// <summary>
    /// Gemini usageMetadata
    /// </summary>
    [JsonPropertyName("usageMetadata")]
    public required UsageMetadataDto UsageMetadata { get; set; }

    /// <summary>
    /// Gemini modelVersion
    /// </summary>
    [JsonPropertyName("modelVersion")]
    public required string ModelVersion { get; set; }
}