using Moq;
using Core.Models;
using Core;
using Domain.Services;

namespace Domain.Tests
{
    public class DocumentServiceTests
    {
        [Fact]
        public async Task Create_Calls_Repository_And_Sets_Cache()
        {
            // Arrange
            var mockCacheService = new Mock<ICacheService>();
            var mockDocumentRepository = new Mock<IDocumentRepository>();

            var documentService = new DocumentService(mockCacheService.Object, mockDocumentRepository.Object);
            var document = new Document { Id = "some-unique-identifier1", Tags = ["important", ".net"], };

            // Act
            await documentService.Create(document);

            // Assert
            mockDocumentRepository.Verify(repo => repo.Create(document), Times.Once);
            mockCacheService.Verify(cache => cache.Set(document.Id, document), Times.Once);
        }

        [Fact]
        public async Task Get_Returns_From_Cache()
        {
            // Arrange
            var mockCacheService = new Mock<ICacheService>();
            var mockDocumentRepository = new Mock<IDocumentRepository>();

            var documentId = "1";
            var document = new Document { Id = "some-unique-identifier1", Tags = ["important", ".net"], };

            mockCacheService.Setup(cache => cache.TryGetValue(documentId, out document))
                            .Returns(true);

            var documentService = new DocumentService(mockCacheService.Object, mockDocumentRepository.Object);

            // Act
            var result = await documentService.Get(documentId);

            // Assert
            Assert.Equal(document, result);
            mockDocumentRepository.Verify(repo => repo.Get(It.IsAny<string>()), Times.Never);
        }
    }
}
