using AutoMapper;
using System.Linq;
using WebApi.DBOperations;

namespace BookStore.Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _Context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _Context = context;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _Context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new System.Exception("Yazar bulunamadı.");
            return _mapper.Map<AuthorDetailViewModel>(author); ;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}
