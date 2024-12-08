namespace SocialMind.Core.LLMApiServices
{
    public interface ILanguageModelService
    {
        Task<T?> GetResponseAsync<T>(string? prompt);
    }
}
