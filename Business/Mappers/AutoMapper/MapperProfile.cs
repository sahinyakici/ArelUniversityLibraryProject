using AutoMapper;
using Business.Mappers.AutoMapper.Resolvers;
using Business.Mappers.AutoMapper.Resolvers.ImageResolver;
using Business.Mappers.AutoMapper.Resolvers.RentalResolver;
using Business.Mappers.AutoMapper.Resolvers.UserOperationClaimResolver;
using Business.Mappers.AutoMapper.Resolvers.UserResolver;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
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
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom<UserNameResolver>())
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ImagePathResolver>());

        CreateMap<BookDTO, Book>()
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom<GenreIdResolver>())
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom<AuthorIdResolver>())
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom<UserIdResolver>());

        CreateMap<UserForRegisterDto, User>().AfterMap((source, dest) =>
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(source.Password, out passwordHash, out passwordSalt);

            dest.UserId = Guid.NewGuid();
            dest.PasswordSalt = passwordSalt;
            dest.PasswordHash = passwordHash;
            dest.Status = false;
        });

        CreateMap<UserOperationClaimDto, UserOperationClaim>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserOperationUserIdResolver>())
            .ForMember(dest => dest.OperationClaimId, opt => opt.MapFrom<UserOperationClaimIdResolver>())
            .ForMember(dest => dest.UserOperationClaimId, opt => opt.MapFrom<UserOperationClaimRowIdResolver>());

        CreateMap<Rental, RentalDTO>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom<RentalDtoOwnerNameResolver>())
            .ForMember(dest => dest.BookName, opt => opt.MapFrom<RentalDtoBookNameResolver>())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom<RentalDtoUserNameResolver>());

        CreateMap<RentalDTO, Rental>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom<RentalDtoBookIdResolver>())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<RentalDtoUserIdResolver>());

        CreateMap<Genre, GenreDTO>()
            .ForMember(dest => dest.BookCount, opt => opt.MapFrom<GenreBookCountResolver>());
        CreateMap<Author, AuthorDTO>().ReverseMap();
    }
}