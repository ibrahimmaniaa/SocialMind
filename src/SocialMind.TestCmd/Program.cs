using SocialMind.Core.Domain;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;
using SocialMind.Core.LLMApiServices;


namespace SocialMind.TestCmd;

internal class Program
{
    public static async Task Main(string[] args)
    {
        const string API_KEY = "AIzaSyAZ-oHoZ6roBFaPC08VLlP95ZCYqOypdtA";

        HttpClient httpClient = new();

        //ILanguageModelService chatService = new MistralApiService(httpClient, API_ENDPOINT, API_KEY);
        ILanguageModelService chatService = new GeminiApiService(httpClient, GeminiConfig.RequestUri, API_KEY);

        try
        {
            ResponseDto? response = await chatService.GetResponseAsync<ResponseDto>("How many hours per day?");

            string responseText = response!.Cadndidates.First().Content.Parts.First().Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}