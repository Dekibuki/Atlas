using Atlas.API.DTO;
using Atlas.API.Responses.RefinitivWorldCheck;
using Atlas.Services.RefinitivWorldCheckService;
using Moq;
using Moq.Protected;
using System.Net;
using Atlas.Domain.Enum;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Atlas.Tests
{
    public class RefinitivWorldCheckTests
    {
        private const int MaximumRetries = 3;

        [Fact]
        public async Task ProcessScreeningSucceess_ReturnsResponse()
        {
            // Arrange
            RefinitivWorldCheckResponse expected = new RefinitivWorldCheckResponse
            {
                CaseId = "case-123",
                Status = ScreeningResult.CLEAR
            };

            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() =>
                {
                    HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
                    StringContent content = new StringContent(JsonSerializer.Serialize(expected), Encoding.UTF8);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    msg.Content = content;
                    return msg;
                })
                .Verifiable();

            HttpClient httpClient = new HttpClient(handlerMock.Object);
            RefinitivWorldCheckService service = new RefinitivWorldCheckService(httpClient);

            // Act
            RefinitivWorldCheckResponse? result = await service.ProcessScreening(new RefinitivWorldCheckScreeningDto());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.CaseId, result!.CaseId);
            handlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task ProcessScreeningFailsOnInternalError_RetriesThreeTimes_ThenThrows()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() =>
                {
                    HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    msg.Content = new StringContent(string.Empty);
                    return msg;
                });

            HttpClient httpClient = new HttpClient(handlerMock.Object);
            RefinitivWorldCheckService service = new RefinitivWorldCheckService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.ProcessScreening(new RefinitivWorldCheckScreeningDto()));

            handlerMock.Protected().Verify("SendAsync", Times.Exactly(MaximumRetries), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task ProcessScreeningFailsOnBadRequest_Throws()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() =>
                {
                    HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    msg.Content = new StringContent(string.Empty);
                    return msg;
                })
                .Verifiable();

            HttpClient httpClient = new HttpClient(handlerMock.Object);
            RefinitivWorldCheckService service = new RefinitivWorldCheckService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.ProcessScreening(new RefinitivWorldCheckScreeningDto()));
            handlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }
    }
}
