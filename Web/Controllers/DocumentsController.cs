using AutoMapper;
using Core;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService documentService;
        private readonly IMapper mapper;

        public DocumentsController(
            IDocumentService documentService,
            IMapper mapper)
        {
            this.documentService = documentService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document document)
        {
            if (Request.ContentType != "application/json")
            {
                return BadRequest("Content-Type must be application/json");
            }

            if (await documentService.Get(document.Id) != null)
            {
                return Conflict(error: $"Document with id {document.Id} already exists.");
            }

            var documentDomain = mapper.Map<Core.Models.Document>(document);
            documentDomain.Data = JsonSerializer.Serialize(document.Data);
            await documentService.Create(documentDomain);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, [FromHeader(Name = "Accept")] string accept = "application/json")
        {
            var document = await documentService.Get(id);
            if (document == null)
            {
                return NotFound();
            }

            switch (accept)
            {
                case "application/xml":
                    return Content(SerializationHelpers.ConvertDocumentToXml(document), "application/xml");

                case "application/x-msgpack":
                    return new FileContentResult(SerializationHelpers.ConvertDocumentToMessagePack(document), "application/x-msgpack");

                case "application/json":
                default:
                    return Content(SerializationHelpers.ConvertDocumentToJson(document), "application/json");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Document document)
        {
            if (Request.ContentType != "application/json")
            {
                return BadRequest("Content-Type must be application/json");
            }

            if (await documentService.Get(document.Id) == null)
            {
                return NotFound($"Document with id {document.Id} does not exists.");
            }

            var documentDomain = mapper.Map<Core.Models.Document>(document);
            documentDomain.Data = JsonSerializer.Serialize(document.Data);
            await documentService.Update(mapper.Map<Core.Models.Document>(documentDomain));

            return NoContent();
        }
    }
}