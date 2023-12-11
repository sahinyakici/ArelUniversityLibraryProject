using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IImageService
{
    IResult Add(Image image);
    IResult Delete(String imagePath);
    IDataResult<Image> GetImageByBookId(Guid bookId, bool withDeleted = false);
}