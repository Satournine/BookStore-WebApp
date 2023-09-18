using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(new Book
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
                }
                );

                context.SaveChanges();
            }
        }
    }
}
