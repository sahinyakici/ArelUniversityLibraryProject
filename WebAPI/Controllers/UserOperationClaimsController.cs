using Business.Abstract;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpGet("GetAllWithUserName")]
        public IActionResult GetAll(string userName, bool withDeleted)
        {
            var result = _userOperationClaimService.GetAllClaimsWithUserName(userName, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(UserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.Add(userOperationClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid userOperationClaimId, bool permanently)
        {
            var result = _userOperationClaimService.Delete(userOperationClaimId, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("DeleteAllClaims")]
        public IActionResult DeleteAllClaims(string userName, bool permanently)
        {
            var result = _userOperationClaimService.DeleteAllClaims(userName, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}