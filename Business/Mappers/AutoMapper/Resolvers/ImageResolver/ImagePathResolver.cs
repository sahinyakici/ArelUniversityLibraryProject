using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.ImageResolver;

public class ImagePathResolver : IValueResolver<Book, BookDTO, String>
{
    private readonly IImageService _imageService;

    public ImagePathResolver(IImageService imageService)
    {
        _imageService = imageService;
    }

    public string Resolve(Book source, BookDTO destination, string destMember, ResolutionContext context)
    {
        var result = _imageService.GetImageByBookId(source.BookId);
        if (result.Success)
        {
            String imagePath = result.Data == null ? "assets/images/default.jpg" : result.Data.ImagePath;
            return imagePath;
        }

        throw new ArgumentException(result.Message);
    }
}