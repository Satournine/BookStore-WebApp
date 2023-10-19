using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;

namespace BookStore.Application.AuthorOperations.Query.GetAuthors
{
    public class GetAuthorsQuery
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            var authorList = _context.Authors.ToList();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return vm;
        }

    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}
