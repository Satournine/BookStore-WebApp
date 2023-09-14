using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //   var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //   return book;
        //}
    }
}
