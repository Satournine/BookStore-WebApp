using FluentValidation;

namespace BookStore.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(4).NotEmpty();
        }
    }
}
