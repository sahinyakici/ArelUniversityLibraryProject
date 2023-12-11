using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll(bool withDeleted)
        {
            var result = _genreService.GetAll(withDeleted);
            if (result.Success)
            {
                List<Genre> genres = result.Data;
                List<GenreDTO> genreDtos = genres.Select(genre => _mapper.Map<GenreDTO>(genre)).ToList();

                return Ok(new SuccessDataResult<List<GenreDTO>>(genreDtos));
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getallwithcount")]
        public ActionResult GetAllWithCount(bool withDeleted)
        {
            var result = _genreService.GetAll(withDeleted);
            if (result.Success)
            {
                List<Genre> genres = result.Data;
                List<GenreDTO> genreDtos = genres.Select(genre => _mapper.Map<GenreDTO>(genre)).ToList();

                return Ok(new SuccessDataResult<List<GenreDTO>>(genreDtos));
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid guid, bool withDeleted)
        {
            var result = _genreService.GetById(guid, withDeleted);
            if (result.Success)
            {
                Genre genre = result.Data;
                GenreDTO genreDTO = _mapper.Map<GenreDTO>(genre);
                return Ok(genreDTO);
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