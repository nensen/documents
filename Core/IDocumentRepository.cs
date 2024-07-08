using Core.Models;

namespace Core
{
    public interface IDocumentRepository
    {
        Task<Document?> Get(string id);

        Task Create(Document document);

        Task Update(Document document);
    }
}