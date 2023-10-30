using AutoMapper;
using BookStore.Application.AuthorOperations.Command.AddAuthor;
using BookStore.Application.AuthorOperations.Query.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Query.GetAuthors;
using BookStore.Application.BookOperations.Query.GetBookDetail;
using BookStore.Application.BookOperations.Query.GetBooks;
using BookStore.Application.GenreOperations.Query.GetGenreDetail;
using BookStore.Application.GenreOperations.Query.GetGenres;
using BookStore.Application.UserOperations.Commands.CreateUserCommand;
using BookStore.Controllers;
using BookStore.Entities;
using static BookStore.Application.BookOperations.Command.AddBook.CreateBookCommand;
using static BookStore.Application.UserOperations.Commands.CreateUserCommand.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<AddAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

            CreateMap<CreateUserModel, Users>();
        }
    }
}
