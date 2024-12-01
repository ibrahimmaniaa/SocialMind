namespace SocialMind.Core.LLMApiServices
{
    public interface ILanguageModelService
    {
        Task<T?> GetResponseAsync<T>(string? prompt);

        string? ResponseParser(string response);
    }
}
