﻿using SocialMind.Core.Domain;
using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;
using SocialMind.Core.LLMApiServices;


namespace SocialMind.TestCmd;

internal class Program
{
    public static async Task Main(string[] args)
    {
        const string API_KEY = "";

        HttpClient httpClient = new();

        //ILanguageModelClient chatService = new MistralApiService(httpClient, API_ENDPOINT, API_KEY);
        ILanguageModelClient geminiChatService = new GeminiApiService(httpClient, GeminiConfig.RequestUri, API_KEY);

        try
        {
            PartDto? response = await geminiChatService.GetResponseAsync<PartDto>("How many hours per day?");

            //string responseText = response!.Cadndidates.First().Content.Parts.First().Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}