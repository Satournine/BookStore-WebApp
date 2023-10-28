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

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public GetBookDetailValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenBookIdIsInvalid_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int bookId = 999;
            GetBookDetailQuery command = new GetBookDetailQuery(_context);
            command.BookId = bookId;
            //act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }
    }
}
