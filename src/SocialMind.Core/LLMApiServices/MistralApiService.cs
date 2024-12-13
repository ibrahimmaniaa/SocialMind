using System.Text.Json;


namespace SocialMind.Core.LLMApiServices;

public class MistralApiService : LanguageModelClientBase
{
    public MistralApiService(HttpClient httpClient, string apiEndpoint, string apiKey)
        : base(httpClient, apiEndpoint, apiKey) { }

    protected override object CreatePayload(string? message)
    {
        return new
               {
                   model = "mistral-large-latest",
                   messages = new[]
                              {
                                  new { role = "user", content = message }
                              }
               };
    }
}