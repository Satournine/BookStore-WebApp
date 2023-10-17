using AutoMapper;
using BookStore.Application.BookOperations.Query.GetBookDetail;
using BookStore.Application.BookOperations.Query.GetBooks;
using BookStore.Application.GenreOperations.Query.GetGenreDetail;
using BookStore.Application.GenreOperations.Query.GetGenres;
using BookStore.Entities;
using static BookStore.Application.BookOperations.Command.AddBook.CreateBookCommand;

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
        }
    }
}
