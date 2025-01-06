namespace SocialMind.Core.Domain.Configs;

/// <summary>
/// Gemini configuration
/// </summary>
public class GeminiConfig : ILanguageModelClientConfig
{
    /// <inheritdoc />
    public string RequestUri => $"{BaseUrl}{Model}{Endpoint}?key={ApiKey}";

    private static string BaseUrl => "https://generativelanguage.googleapis.com/v1beta/models/";

    private static string Model => "gemini-1.5-flash";

    private static string Endpoint => ":generateContent";

    private static string ApiKey => "";
}