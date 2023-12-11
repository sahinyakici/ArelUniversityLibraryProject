using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class ImageManager : IImageService
{
    private IImageDal _imageDal;

    public ImageManager(IImageDal imageDal)
    {
        _imageDal = imageDal;
    }

    public IResult Add(Image image)
    {
        image.ImageId = Guid.NewGuid();
        _imageDal.Add(image);
        return new SuccessResult();
    }

    public IResult Delete(String imagePath)
    {
        Image willDeleteImage = _imageDal.Get(image => image.ImagePath == imagePath);
        _imageDal.Delete(willDeleteImage);
        return new SuccessResult(Messages.ImageDeleted);
    }

    public IDataResult<Image> GetImageByBookId(Guid bookId, bool withDeleted = false)
    {
        Image image = _imageDal.Get(image => image.BookId == bookId && (withDeleted || !image.IsDeleted));
        return new SuccessDataResult<Image>(image, Messages.ImageFound);
    }
}