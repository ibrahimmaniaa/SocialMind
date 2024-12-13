using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace SocialMind.Core.Services;

public abstract class LanguageModelClientBase : ILanguageModelClient
{
    protected HttpClient HttpClient;

    private readonly string apiEndpoint;

    private readonly string apiKey;


    protected LanguageModelClientBase(HttpClient httpClient, string apiEndpoint, string apiKey)
    {
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.apiEndpoint = apiEndpoint ?? throw new ArgumentNullException(nameof(apiEndpoint));
        this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }


    /// <summary>
    /// Establishes the payload to be sent to the API as
    /// each API has a different payload structure.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected abstract object CreatePayload(string message);


    /// <summary>
    /// The url to be constructed for the http request.
    /// </summary>
    /// <returns></returns>
    protected virtual string ConstructUrl() => apiEndpoint;


    /// <summary>
    /// Setting the default headers for the http request.
    /// </summary>
    protected virtual void SetHeaders()
    {
        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }


    /// <inheritdoc />
    public async Task<T> GetResponseAsync<T>(string message)
    {
        string url = ConstructUrl();

        object payload = CreatePayload(message);

        StringContent jsonContent = new(JsonSerializer.Serialize(payload),
                                        Encoding.UTF8,
                                        "application/json");

        SetHeaders();

        try
        {
            HttpResponseMessage response = await HttpClient.PostAsync(url, jsonContent);

            response.EnsureSuccessStatusCode();

            T? result = await response.Content.ReadFromJsonAsync<T>();

            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error sending request to API: {ex.Message}");
        }
    }
}