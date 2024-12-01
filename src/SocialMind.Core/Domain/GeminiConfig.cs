namespace SocialMind.Core.Domain;

/// <summary>
/// Gemini configuration
/// </summary>
public class GeminiConfig
{
    /// <summary>
    /// Entire request uri for generating content
    /// </summary>
    public static string RequestUri => $"{BaseUrl}{Model}{Endpoint}";

    private static string BaseUrl => "https://generativelanguage.googleapis.com/v1beta/models/";

    private static string Model => "gemini-1.5-flash";

    private static string Endpoint => ":generateContent";
}