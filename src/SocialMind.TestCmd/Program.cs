using SocialMind.Core.LLMApiServices;


namespace SocialMind.TestCmd;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // Provide the API endpoint and API key
        const string API_ENDPOINT = "";
        const string API_KEY = "";

        HttpClient httpClient = new();

        ILanguageModelService chatService = new MistralApiService(httpClient, API_ENDPOINT, API_KEY);

        try
        {
            Console.WriteLine("Enter a message: ");
            string? message = Console.ReadLine();
            string response = await chatService.GetResponseAsync(message);
            string? responseText = chatService.ResponseParser(response);

            Console.WriteLine("Response: " + responseText);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadKey();
    }
}