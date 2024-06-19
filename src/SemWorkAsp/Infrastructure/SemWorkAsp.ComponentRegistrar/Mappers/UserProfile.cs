using AutoMapper;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.ComponentRegistrar.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        
        CreateMap<CreateUserRequest, User>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.Role, map => map.MapFrom(s => "user"));
    }
}