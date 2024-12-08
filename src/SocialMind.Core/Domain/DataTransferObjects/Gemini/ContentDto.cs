using System.Text.Json.Serialization;


namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini content DTO
/// </summary>
public class ContentDto
{
    /// <summary>
    /// Gemini parts
    /// </summary>
    [JsonPropertyName("parts")]
    public required PartDto[] Parts { get; set; }

    /// <summary>
    /// Gemini role
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}