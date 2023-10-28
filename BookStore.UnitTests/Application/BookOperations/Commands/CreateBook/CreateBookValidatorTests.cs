using AutoMapper;
using BookStore.Application.BookOperations.Command.AddBook;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using Xunit;
using static BookStore.Application.BookOperations.Command.AddBook.CreateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Elric of Melnibone", 0, 0)]
        [InlineData("Elric of Melnibone", 0, 1)]
        [InlineData("Elric of Melnibone", 1, 0)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBEReturnErrors(string title, int pageCount, int genreId)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-1)
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Elric of Melnibone",
                GenreId = 1,
                PageCount = 250,
                PublishDate = DateTime.Now.Date
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);
            error.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Elric of Melnibone",
                GenreId = 1,
                PageCount = 250,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);
            error.Errors.Count.Should().Be(0);

        }
    }
}
