using FluentValidation;

using Folks.ChatService.Domain.Constants;

namespace Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MinimumLength(GroupSettings.TitleMinimumLength)
            .MaximumLength(GroupSettings.TitleMaximumLength);
    }
}
