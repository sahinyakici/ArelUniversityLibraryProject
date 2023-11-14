using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IRentalService
{
    IDataResult<List<RentalDTO>> GetRentalsByUserName(string userName);
    IDataResult<List<RentalDTO>> GetRentalsByUserId(Guid userId);
    IDataResult<List<RentalDTO>> GetRentalsByBookId(Guid bookId);
    IDataResult<RentalDTO> GetRentalById(Guid rentalId);
    IResult Add(Rental rental);
    IResult Update(Rental rental);
    IResult Delete(Rental rental, bool softDelete = true);
}