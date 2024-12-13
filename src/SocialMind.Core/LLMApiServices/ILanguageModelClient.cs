namespace SocialMind.Core.LLMApiServices;

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
    /// <returns>Task</returns>
    Task<T> GetResponseAsync<T>(string prompt);
}