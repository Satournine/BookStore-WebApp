using AutoMapper;
using BookStore.Application.AuthorOperations.Command.AddAuthor;
using BookStore.Application.AuthorOperations.Command.DeleteAuthor;
using BookStore.Application.AuthorOperations.Command.UpdateAuthor;
using BookStore.Application.AuthorOperations.Query.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Query.GetAuthors;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
            //GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            //var obj = query.Handle();
            //return Ok(obj);
        }
        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] AddAuthorModel newAuthor)
        {
            AddAuthorCommand command = new AddAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            AddAuthorValidator validator = new AddAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updateAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
