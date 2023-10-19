using FluentValidation;

namespace BookStore.Application.AuthorOperations.Command.AddAuthor
{
    public class AddAuthorValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(x => x.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(x => x.Model.Birthday).NotEmpty().LessThan(System.DateTime.Now);
        }
    }
}
