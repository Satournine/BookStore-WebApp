using BookStore.Application.BookOperations.Command.UpdateBook;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly CommonTestFixture _testFixture;

        public UpdateBookValidatorTests(CommonTestFixture testFixture)
        {
            _testFixture = testFixture;
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "Hobbit",
                GenreId = 1,
            };
            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
