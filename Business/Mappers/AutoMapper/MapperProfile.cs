using AutoMapper;
using Business.Mappers.AutoMapper.Resolvers;
using Business.Mappers.AutoMapper.Resolvers.UserResolver;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Book, BookDTO>()
            .ForMember(
                dest => dest.GenreName, opt => opt.MapFrom<GenreNameResolver>())
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom<AuthorNameResolver>())
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom<UserNameResolver>());

        CreateMap<BookDTO, Book>()
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom<GenreIdResolver>())
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom<AuthorIdResolver>())
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom<UserIdResolver>())
            ;
    }
}