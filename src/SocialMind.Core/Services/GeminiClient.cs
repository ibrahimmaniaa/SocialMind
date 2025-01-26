using System.Net;
using System.Net.Http.Headers;

using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;


namespace SocialMind.Core.Services;

public class GeminiClient : LanguageModelClientBase
{
    /// <inheritdoc />
    public GeminiClient(HttpClient httpClient, GeminiConfig clientConfig) : base(httpClient, clientConfig) { }


    protected override object CreatePayload(string message)
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