using AutoMapper;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace Web.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Core.Models.Document, Models.Document>()
                .ForMember(src => src.Data, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}