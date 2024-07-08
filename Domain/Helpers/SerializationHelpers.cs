using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Domain.Helpers
{
    public static class SerializationHelpers
    {
        public static string ConvertDocumentToXml(Core.Models.Document document)
        {
            Func<string, IEnumerable<XElement>> convertJsonDataToXml = jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    return Enumerable.Empty<XElement>();
                }

                XmlDocument? xmlDocument = JsonConvert.DeserializeXmlNode(jsonData, "Root");
                if (xmlDocument == null)
                {
                    throw new InvalidOperationException("Failed to deserialize JSON to XML.");
                }

                return xmlDocument.DocumentElement!.ChildNodes.Cast<XmlNode>()
                    .Select(node => XElement.Parse(node.OuterXml));
            };

            var root = new XElement("document",
                new XElement("id", document.Id),
                new XElement("tags", document.Tags.Select(tag => new XElement("tag", tag))),
                new XElement("data", convertJsonDataToXml(document.Data))
            );

            return root.ToString();
        }

        public static byte[] ConvertDocumentToMessagePack(Core.Models.Document document)
        {
            Func<string, object?> convertJsonDataToObject = jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    return null;
                }

                return MessagePackSerializer.Deserialize<object>(MessagePackSerializer.ConvertFromJson(jsonData));
            };

            var documentObject = new
            {
                id = document.Id,
                tags = document.Tags,
                data = convertJsonDataToObject(document.Data)
            };

            return MessagePackSerializer.Serialize(documentObject);
        }

        public static string ConvertDocumentToJson(Core.Models.Document document)
        {
            Func<string, object> convertJsonDataToObject = jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    return new { };
                }

                return JsonConvert.DeserializeObject<object>(jsonData)!;
            };

            var documentObject = new
            {
                id = document.Id,
                tags = document.Tags,
                data = convertJsonDataToObject(document.Data)
            };

            return JsonConvert.SerializeObject(documentObject);
        }
    }
}