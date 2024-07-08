using Core.Models;

namespace Core
{
    public interface IDocumentService
    {
        Task Create(Document document);

        Task Update(Document document);

        Task<Document?> Get(string id);
    }
}