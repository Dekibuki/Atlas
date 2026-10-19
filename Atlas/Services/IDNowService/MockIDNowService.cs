using Atlas.API.DTO;
using Atlas.API.Responses.IDNow;
using Atlas.Domain.Enum;

namespace Atlas.Services.IDNowService
{
    public class MockIDNowService : IIDNowService
    {
        public Task<IDNowDocumentResponse?> ProcessIdentifications(IDNowDocumentDto customerDocumentData)
        {
            return Task.FromResult(new IDNowDocumentResponse
            {
                IdentificationId = Guid.NewGuid().ToString(),
                DocumentResult = DocumentResult.VALID,
                Confidance = 0.98f,
                FaceMatch = true
            });
        }
    }
}
