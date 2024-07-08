using AutoMapper;
using Core;
using Core.Models;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo
{
    public class MongoDocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<Models.Document> documents;
        private readonly IMapper mapper;

        public MongoDocumentRepository(
            MongoDbContext context,
            IMapper mapper)
        {
            this.documents = context.Documents;
            this.mapper = mapper;
        }

        public async Task<Document?> Get(string id)
        {
            var document = await documents.Find(doc => doc.Id == id).FirstOrDefaultAsync();

            return document != null ? mapper.Map<Document>(document) : null;
        }

        public async Task Create(Document document)
        {
            var x = mapper.Map<Models.Document>(document);
            await documents.InsertOneAsync(x);
        }

        public async Task Update(Document document)
        {
            await documents.ReplaceOneAsync(doc => doc.Id == document.Id, mapper.Map<Models.Document>(document));
        }
    }
}