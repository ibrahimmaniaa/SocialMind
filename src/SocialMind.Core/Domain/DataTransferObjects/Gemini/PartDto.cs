using System.Text.Json.Serialization;


namespace SocialMind.Core.Domain.DataTransferObjects.Gemini;

/// <summary>
/// Gemini Part DTO
/// </summary>
public class PartDto
{
    /// <summary>
    /// Gemini text
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}