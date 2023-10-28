using AutoMapper;
using BookStore.Application.BookOperations.Command.DeleteBook;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void Handle_WhenValidBookIdIsGiven_BookShouldBeDeleted()
        {
            // Arrange
            int bookIdToDelete = 1;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookIdToDelete;
            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var deletedBook = _context.Books.FirstOrDefault(b => b.Id == bookIdToDelete);
            deletedBook.Should().BeNull();
        }
    }
}
