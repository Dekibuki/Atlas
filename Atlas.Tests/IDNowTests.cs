using Atlas.API.DTO;
using Atlas.API.Responses.IDNow;
using Atlas.Services.IDNowService;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Atlas.Tests
{
    public class IDNowTests
    {
        private const int MaximumAttempts = 3;

        [Fact]
        public async Task ProcessIdentificationsSuccess_ReturnsResponse()
        {
            // Arrange
            IDNowDocumentResponse expected = new IDNowDocumentResponse
            {
                IdentificationId = "abc",
                DocumentResult = Atlas.Domain.Enum.DocumentResult.VALID,
                FaceMatch = true,
                Confidance = 0.9f
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
            IDNowService service = new IDNowService(httpClient);

            // Act
            IDNowDocumentResponse? result = await service.ProcessIdentifications(new IDNowDocumentDto());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.IdentificationId, result!.IdentificationId);
            handlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task ProcessIdentificationsFailsDueToInternalError_RetriesThreeTimes_ThenThrows()
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
            IDNowService service = new IDNowService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.ProcessIdentifications(new IDNowDocumentDto()));
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(MaximumAttempts), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task ProcessIdentificationsFailsDueToBadRequest_Throws()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() =>
                {
                    HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    msg.Content = new StringContent(string.Empty);
                    return msg;
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new IDNowService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.ProcessIdentifications(new IDNowDocumentDto()));
            handlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }
    }
}
