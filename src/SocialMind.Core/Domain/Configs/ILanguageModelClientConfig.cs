namespace SocialMind.Core.Domain.Configs;

/// <summary>
/// Language model client configuration
/// </summary>
public interface ILanguageModelClientConfig
{
    /// <summary>
    /// Entire request uri for generating content
    /// </summary>
    string RequestUri { get; }
}