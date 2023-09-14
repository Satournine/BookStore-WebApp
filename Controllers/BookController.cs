using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "The Fellowship of the Ring",
                Genre = "Fantasy",
                PageCount = 423,
                PublishDate = new DateTime(1954, 7, 29)
            },
            new Book
            {
                Id = 2,
                Title = "The Two Towers",
                Genre = "Fantasy",
                PageCount = 352,
                PublishDate = new DateTime(1954, 11, 11)
            },
            new Book
            {
                Id = 3,
                Title = "Dune",
                Genre = "Science Fiction",
                PageCount = 412,
                PublishDate = new DateTime(1965, 6, 1)
            },
        };


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(x=> x.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "A book with the same title already exists."
                };
                return BadRequest();
            }
            BookList.Add(newBook);
            return StatusCode(201);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBook(int id, [FromBody] JsonPatchDocument<Book> patchedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
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

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
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
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found."
                };
                return NotFound(errorResponse);
            }
            BookList.Remove(book);
            return Ok();
        }
    }
}
