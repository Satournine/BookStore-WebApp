using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace BookStore.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar bulunamadı.");
            }
            if(_context.Authors.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı isimde yazar mevcut.");

            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            _context.SaveChanges();
            
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
