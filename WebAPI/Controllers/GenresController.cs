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

        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            var result = _genreService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(Guid guid)
        {
            var result = _genreService.GetById(guid);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("add")]
        public IActionResult Add(Genre genre)
        {
            var result = _genreService.Add(genre);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }
    }
}