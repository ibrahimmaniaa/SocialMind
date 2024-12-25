using System.Net;
using System.Net.Http.Json;
using System.Text;

using Moq;
using Moq.Protected;

using SocialMind.Core.Domain.Configs;
using SocialMind.Core.Domain.DataTransferObjects.Gemini;

namespace SocialMind.Core.Tests.Services;

public class LanguageModelClientBaseTests
{
    [Fact]
    public void GetResponseAsync_WhenJsonDeserializingReturnsNullTest()
    {
        Mock<ILanguageModelClientConfig> mockLanguageModelClientConfig = new();

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Invalid Json text", Encoding.UTF8, "application/json")
            });

        HttpClient fakeHttpClient = new(mockHttpMessageHandler.Object);

        FakeLanguageModelClient fakeLanguageModelClient = new(fakeHttpClient, mockLanguageModelClientConfig.Object);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await fakeLanguageModelClient.GetResponseAsync<ResponseDto>("dummy message"));
    }
}