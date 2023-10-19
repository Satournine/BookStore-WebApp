using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;


namespace BookStore.Application.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}
