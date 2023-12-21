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
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IAuthorService _authorManager;

        public AuthorsController(IAuthorService authorManager, IMapper mapper)
        {
            _authorManager = authorManager;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(bool withDeleted)
        {
            var result = _authorManager.GetAll(withDeleted);
            if (result.Success)
            {
                List<AuthorDTO> authorDTOs = result.Data.Select(author => _mapper.Map<AuthorDTO>(author)).ToList();
                return Ok(new SuccessDataResult<List<AuthorDTO>>(authorDTOs));
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid guid, bool withDeleted)
        {
            var result = _authorManager.GetById(guid, withDeleted);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(Author author)
        {
            var result = _authorManager.Add(author);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("Update")]
        public IActionResult Update(Author author)
        {
            var result = _authorManager.Update(author);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid authorId, bool permanently = false)
        {
            var result = _authorManager.Delete(authorId, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}