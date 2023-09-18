using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore;
using WebApi.DBOperations;



namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x=> x.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "A book with the same title already exists."
                };
                return BadRequest();
            }
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return StatusCode(201);
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
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
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
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.Genre = updatedBook.Genre != default ? updatedBook.Genre : book.Genre;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
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
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
