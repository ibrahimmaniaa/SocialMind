using System.Text.Json;


namespace SocialMind.Core.LLMApiServices;

public class GeminiApiService : LLMApiService
{
    private readonly string apiUrl;


    public GeminiApiService(HttpClient httpClient,
                            string apiEndpoint,
                            string apiKey)
        : base(httpClient, apiEndpoint, apiKey)
    {
        apiUrl = $"{apiEndpoint}{apiKey}";
    }


    protected override string ConstructUrl() => apiUrl;


    protected override void SetHeaders()
    {
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }


    protected override object CreatePayload(string? message)
    {
        return new
               {
                   contents = new[]
                              {
                                  new
                                  {
                                      text = message
                                  }
                              }
               };
    }


    public override string? ResponseParser(string response)
    {
        using JsonDocument jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("candidates")[0]
                      .GetProperty("content")
                      .GetProperty("parts")[0]
                      .GetProperty("text")
                      .GetString();
    }
}