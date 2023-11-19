using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("GetRentalsByUserName")]
        public IActionResult GetRentalsByUserName(string userName, bool withDeleted)
        {
            var result = _rentalService.GetRentalsByUserName(userName, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetRentalsByUserId")]
        public IActionResult GetRentalByUserId(Guid userId, bool withDeleted)
        {
            var result = _rentalService.GetRentalsByUserId(userId, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetRentalsByBookId")]
        public IActionResult GetRentalsByBookId(Guid bookId, bool withDeleted)
        {
            var result = _rentalService.GetRentalsByBookId(bookId, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetRentalById")]
        public IActionResult GetRentalById(Guid rentalId, bool withDeleted)
        {
            var result = _rentalService.GetRentalById(rentalId, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("Update")]
        public IActionResult Update(RentalDTO rentalDto)
        {
            var result = _rentalService.Update(rentalDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("CancelRental")]
        public IActionResult CancelRental(Guid rentalId)
        {
            var result = _rentalService.CancelRental(rentalId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteRental(Guid id, bool permanently)
        {
            var result = _rentalService.Delete(id, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}