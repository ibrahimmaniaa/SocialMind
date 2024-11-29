using System.Text;
using System.Text.Json;

namespace SocialMind.Core.LLMApiServices
{
    public abstract class LLMApiService : ILanguageModelService
    {
        protected HttpClient httpClient;
        private readonly string apiEndpoint;
        private readonly string apiKey;

        protected LLMApiService(HttpClient httpClient, string apiEndpoint, string apiKey)
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
        protected abstract object CreatePayload(string? message);

        /// <summary>
        /// Parsing the response from the API to get only the output text.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public abstract string? ResponseParser(string response);

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
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// Sends a payload to the API and returns the response in
        /// JSON format.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> GetResponseAsync(string? message)
        {
            string url = ConstructUrl();

            object payload = CreatePayload(message);

            StringContent jsonContent = new(JsonSerializer.Serialize(payload),
                                                Encoding.UTF8,
                                                "application/json");

            // Set headers
            SetHeaders();

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error sending request to API: {ex.Message}");
            }
        }
    }
}