using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(bool withDeleted)
        {
            var result = _userService.GetAll(withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid id, bool withDeleted)
        {
            var result = _userService.GetById(id, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetByUserName")]
        public IActionResult GetByName(string userName, bool withDeleted)
        {
            var result = _userService.GetByUserName(userName, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email, bool withDeleted)
        {
            var result = _userService.GetByMail(email, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("Update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid userId, bool permanently)
        {
            var result = _userService.Delete(userId, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}