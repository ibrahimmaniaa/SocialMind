using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Services;

namespace SocialMind.Core.Tests.Services;

internal class FakeLanguageModelClient: LanguageModelClientBase
{
    public FakeLanguageModelClient(HttpClient httpClient, ILanguageModelClientConfig clientConfig) : base(httpClient, clientConfig) { }


    protected override object CreatePayload(string message)
    {
        return new { message };
    }
}