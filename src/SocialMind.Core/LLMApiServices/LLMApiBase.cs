using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using SocialMind.Core.Domain.DataTransferObjects.Gemini;


namespace SocialMind.Core.LLMApiServices
{
    public abstract class LLMApiBase : ILanguageModelService
    {
        protected HttpClient httpClient;
        private readonly string apiEndpoint;
        private readonly string apiKey;

        protected LLMApiBase(HttpClient httpClient, string apiEndpoint, string apiKey)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.apiEndpoint = apiEndpoint ?? throw new ArgumentNullException(nameof(apiEndpoint));
            this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        /// <summary>
        /// Establishes the payload to be sent to the API as
        /// each API has a different payload structure.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected abstract object? CreatePayload(string? message);

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
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// Sends a payload to the API and returns the response in
        /// JSON format.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<T?> GetResponseAsync<T>(string? message)
        {
            string url = ConstructUrl();

            object? payload = CreatePayload(message);

            StringContent jsonContent = new(JsonSerializer.Serialize(payload),
                                                Encoding.UTF8,
                                                "application/json");

            SetHeaders();

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error sending request to API: {ex.Message}");
            }
        }
    }
}