using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore;
using WebApi.DBOperations;
using static BookStore.Application.BookOperations.Command.AddBook.CreateBookCommand;
using static BookStore.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using BookStore.Entities;
using BookStore.Application.BookOperations.Command.AddBook;
using BookStore.Application.BookOperations.Command.DeleteBook;
using BookStore.Application.BookOperations.Command.UpdateBook;
using BookStore.Application.BookOperations.Query.GetBookDetail;
using BookStore.Application.BookOperations.Query.GetBooks;
using BookStore.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBook(int id, [FromBody] JsonPatchDocument<Book> patchedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found."
                };
                return NotFound(errorResponse);
            }

            patchedBook.ApplyTo(book, ModelState);
            if (!ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Invalid",
                };
                return BadRequest(errorResponse);
            }
            _context.SaveChanges();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
