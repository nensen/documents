using Domain.Helpers;
using Core.Models;

namespace Domain.Tests
{
    public class SerializationHelpersTests
    {
        [Fact]
        public void ConvertDocumentToXml_ValidDocument_ReturnsValidXml()
        {
            // Arrange
            var document = new Document
            {
                Id = "some-unique-identifier1",
                Tags = ["important", ".net"],
                Data = "{\"some\": \"data\", \"optional\": \"fields\"}"
            };

            // Act
            string xml = SerializationHelpers.ConvertDocumentToXml(document);

            // Assert   
            Assert.NotNull(xml);
            Assert.Contains("<id>some-unique-identifier1</id>", xml);
            Assert.Contains("<tag>important</tag>", xml);
            Assert.Contains("<tag>.net</tag>", xml);
            Assert.Contains("<some>data</some>", xml);
            Assert.Contains("<optional>fields</optional>", xml);
        }
    }
}