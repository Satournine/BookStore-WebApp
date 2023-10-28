using BookStore.Application.BookOperations.Command.UpdateBook;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using Xunit;
using static BookStore.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void Handle_WhenValidBookIdIsGiven_BookShouldBeUpdated()
        {
            // Arrange
            int bookIdToUpdate = 3;
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookIdToUpdate;
            command.Model = new UpdateBookModel()
            {
                Title = "Dune",
                GenreId = 2,
            };
            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var updatedBook = _context.Books.FirstOrDefault(b => b.Id == bookIdToUpdate);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(command.Model.Title);
            updatedBook.GenreId.Should().Be(command.Model.GenreId);

        }
    }
}
