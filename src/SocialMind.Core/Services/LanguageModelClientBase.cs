using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Domain.DataTransferObjects;


namespace SocialMind.Core.Services;

public abstract class LanguageModelClientBase : ILanguageModelClient
{
    protected HttpClient HttpClient;

    protected ILanguageModelClientConfig ClientConfig;


    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="httpClient">Http client</param>
    /// <param name="clientConfig">Client configuration</param>
    protected LanguageModelClientBase(HttpClient httpClient, ILanguageModelClientConfig clientConfig)
    {
        HttpClient = httpClient;
        ClientConfig = clientConfig;

        SetDefaultHeaders();
    }


    /// <inheritdoc />
    public async Task<T> GetResponseAsync<T>(string prompt) where T : ResponseDtoBase
    {
        object payload = CreatePayload(prompt);

        StringContent jsonContent = new(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await HttpClient.PostAsync(ClientConfig.RequestUri, jsonContent);

            response.EnsureSuccessStatusCode();

            T? result = await response.Content.ReadFromJsonAsync<T>();

            if (result != null)
            {
                return result;
            }

            throw new InvalidOperationException($"Http response succeeded, but the read method of {nameof(HttpResponseMessage.Content)} returned null.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error sending request to API: {ex.Message}");
        }
    }


    /// <summary>
    /// Establishes the payload to be sent to the API as each API has a different payload structure
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns>Payload object</returns>
    protected abstract object CreatePayload(string message);


    private void SetDefaultHeaders()
    {
        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

}