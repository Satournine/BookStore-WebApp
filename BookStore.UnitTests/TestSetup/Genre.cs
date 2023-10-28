using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace BookStore.UnitTests.TestSetup
{
    public static class Genre
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                               new BookStore.Entities.Genre
                               {
                                   Name = "Fantasy",
                               },
                               new BookStore.Entities.Genre
                               {
                                   Name = "Science Fiction",
                               },
                               new BookStore.Entities.Genre
                               {
                                   Name = "Philosophy",
                               }
                                  );
        }
    }
}
