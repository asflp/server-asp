using AutoMapper;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.ComponentRegistrar.Mappers;

public class AdvertisementProfile : Profile
{
    public AdvertisementProfile()
    {
        CreateMap<Advertisement, AdvertisementShortDto>()
            .ForMember(s => s.Username, map => map.MapFrom(s => s.User.Name));

        CreateMap<CreateAdvertisementRequest, Advertisement>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(dest => dest.PhotoUrls, opt => opt.MapFrom(src => new List<string>()));
    }
}