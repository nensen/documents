using AutoMapper;

namespace Infrastructure.Data.Mongo.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Core.Models.Document, Models.Document>()
                .ReverseMap();
        }
    }
}
