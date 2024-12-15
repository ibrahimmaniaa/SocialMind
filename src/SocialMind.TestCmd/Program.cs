using SocialMind.Core.Domain;
using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;
using SocialMind.Core.Services;


namespace SocialMind.TestCmd;

internal class Program
{
    public static async Task Main(string[] args)
    {
        HttpClient httpClient = new();

        ILanguageModelClient geminiClient = new GeminiClient(httpClient, GeminiConfig.RequestUri, GeminiConfig.ApiKey);

        try
        {
            ResponseDto? response = await geminiClient.GetResponseAsync<ResponseDto>("How many hours per day?");

            string responseText = response!.Cadndidates.First().Content.Parts.First().Text!;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}