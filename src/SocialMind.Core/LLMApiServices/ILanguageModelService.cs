namespace SocialMind.Core.LLMApiServices
{
    public interface ILanguageModelService
    {
        Task<string> GetResponseAsync(string? message);

        string? ResponseParser(string response);
    }
}
