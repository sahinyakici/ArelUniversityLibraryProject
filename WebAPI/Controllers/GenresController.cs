using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll(bool withDeleted)
        {
            var result = _genreService.GetAll(withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid guid, bool withDeleted)
        {
            var result = _genreService.GetById(guid, withDeleted);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(Genre genre)
        {
            var result = _genreService.Add(genre);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("Update")]
        public IActionResult Update(Genre genre)
        {
            var result = _genreService.Update(genre);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid genreId, bool permanently)
        {
            var result = _genreService.Delete(genreId, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}