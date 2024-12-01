using System.Text.Json.Serialization;


namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini Request DTO
/// </summary>
public class RequestDto
{
    /// <summary>
    /// Gemini contents
    /// </summary>
    [JsonPropertyName("contents")]
    public ContentDto[] Contents { get; set; }
}