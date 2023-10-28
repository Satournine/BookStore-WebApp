using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace BookStore.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;

        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var book = _context.Books.Where(x => x.AuthorId == AuthorId).Any();
            if (book)
            {
                throw new InvalidOperationException("Yazarın kitabı bulunmaktadır. Yazar silinemez.");
            }
            if (author is null)
            {
                throw new InvalidOperationException("Yazar bulunamadı.");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
