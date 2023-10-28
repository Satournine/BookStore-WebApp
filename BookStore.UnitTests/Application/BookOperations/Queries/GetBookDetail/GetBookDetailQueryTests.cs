using AutoMapper;
using BookStore.Application.BookOperations.Query.GetBookDetail;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using Xunit;
using static BookStore.Application.BookOperations.Query.GetBookDetail.GetBookDetailQuery;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenBookIdIsInvalid_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = 999;

            //act & Assert

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }
    }
}
