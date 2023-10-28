using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;

namespace BookStore.Application.GenreOperations.Query.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _context;

        public readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
