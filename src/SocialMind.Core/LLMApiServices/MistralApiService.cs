using System.Text.Json;


namespace SocialMind.Core.LLMApiServices;

public class MistralApiService : LLMApiService
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

    public override string? ResponseParser(string response)
    {
        using JsonDocument jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
    }
}