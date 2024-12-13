using System.Text.Json;


namespace SocialMind.Core.LLMApiServices;

public class CohereApiService : LanguageModelClientBase
{
    public CohereApiService(HttpClient httpClient, string apiEndpoint, string apiKey)
        : base(httpClient, apiEndpoint, apiKey) { }

    protected override object CreatePayload(string? message)
    {
        return new
               {
                   model = "command-r-plus-08-2024",
                   messages = new[]
                              {
                                  new
                                  {
                                      role = "user",
                                      content = message
                                  }
                              }
               };
    }
}