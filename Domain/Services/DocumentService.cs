using Core;
using Core.Models;

namespace Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ICacheService cacheService;
        private readonly IDocumentRepository documentRepository;

      
        public DocumentService(
            ICacheService cacheService,
            IDocumentRepository documentRepository)
        {
            this.cacheService = cacheService;
            this.documentRepository = documentRepository;
        }

        public async Task Create(Document document)
        {
            await documentRepository.Create(document);
            cacheService.Set(document.Id, document);
        }

        public async Task Update(Document document)
        {
            await documentRepository.Update(document);
            cacheService.Set(document.Id, document);
        }

        public async Task<Document?> Get(string id)
        {
            if (!cacheService.TryGetValue(id, out Document? document))
            {
                document = await documentRepository.Get(id);
                if (document != null)
                {
                    cacheService.Set(id, document);
                }
            }

            return document;
        }
    }
}