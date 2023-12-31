﻿using BookStore.Entities;
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
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre
                    {
                        //Id = 1,
                        Name = "Fantasy",
                    },
                    new Genre
                    {
                        //Id = 2,
                        Name = "Science Fiction",
                    },
                    new Genre
                    {
                        //Id = 3,
                        Name = "Philosophy",
                    }
                );
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Michael",
                        Surname = "Moorcock",
                        Birthday = new DateTime(1939, 12, 18),
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        Birthday = new DateTime(1920, 10, 8),
                    },
                    new Author
                    {
                        Name = "Miyamoto",
                        Surname = "Musashi",
                        Birthday = new DateTime(1584, 3, 19),
                    });
                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Book of 5 Rings",
                        AuthorId = 3,
                        GenreId = 3, //	Philosophy
                        PageCount = 128,
                        PublishDate = new DateTime(1645, 1, 1),
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Elric of Melnibone",
                        AuthorId = 1,
                        GenreId = 1, //	Philosophy
                        PageCount = 250,
                        PublishDate = new DateTime(1972, 1, 1),
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        AuthorId = 2,
                        GenreId = 2, //	Science-Fiction
                        PageCount = 879,
                        PublishDate = new DateTime(1965, 1, 1),
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
