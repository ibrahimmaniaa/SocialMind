﻿using System.Text.Json;

using SocialMind.Core.Domain.DataTransferObjects.Gemini;


namespace SocialMind.Core.LLMApiServices;

public class GeminiApiService : LLMApiBase
{
    private readonly string apiUrl;

    public GeminiApiService(HttpClient httpClient,
                            string apiEndpoint,
                            string apiKey)
        : base(httpClient, apiEndpoint, apiKey)
    {
        apiUrl = $"{apiEndpoint}?key={apiKey}";
    }

    protected override string ConstructUrl() => apiUrl;

    protected override void SetHeaders()
    {
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    protected override object? CreatePayload(string message)
    {
        return new RequestDto
               {
                   Contents =
                   [
                       new ContentDto
                                  {
                                      Parts =
                                      [
                                          new PartDto { Text = message }
                                      ]
                                  }
                   ]
               };
    }
}