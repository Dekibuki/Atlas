using Atlas.API.DTO;
using Atlas.API.Responses;

namespace Atlas.Services.IDNowService
{
    /// <summary>
    /// Interface regarding IDNow functionality - processing document authenticity and face match.
    /// </summary>
    public interface IIDNowService
    {
        public Task<IDNowDocumentResponse?> ProcessCustomerDocumentAndSelfieImages(IDNowDocumentDTO customerDocumentData);
    }
}
