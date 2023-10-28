using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace BookStore.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                                new Book { Title = "Book of 5 Rings", GenreId = 3, PageCount = 128, PublishDate = new DateTime(1645, 1, 1) },
                                new Book { Title = "Elric of Melnibone", GenreId = 1, PageCount = 250, PublishDate = new DateTime(1972, 1, 1) },
                                new Book { Title = "Dune", GenreId = 2, PageCount = 879, PublishDate = new DateTime(1965, 1, 1) }
                                  );
        }
    }
}