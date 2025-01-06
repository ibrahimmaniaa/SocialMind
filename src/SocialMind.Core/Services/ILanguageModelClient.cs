using SocialMind.Core.Domain.DataTransferObjects;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;

namespace SocialMind.Core.Services;

/// <summary>
/// Language model client interface
/// </summary>
public interface ILanguageModelClient
{
    /// <summary>
    /// Get response
    /// </summary>
    /// <typeparam name="T">Response Type</typeparam>
    /// <param name="prompt">Message to be sent to the large language model</param>
    /// <returns>Task of generic type <typeparamref name="T"/></returns>
    Task<T> GetResponseAsync<T>(string prompt) where T : ResponseDtoBase;
}