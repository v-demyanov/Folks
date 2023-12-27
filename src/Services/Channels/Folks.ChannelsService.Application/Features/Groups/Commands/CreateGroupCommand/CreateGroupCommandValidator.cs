using FluentValidation;

using Folks.ChannelsService.Domain.Constants;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator(ChannelsServiceDbContext dbContext)
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MinimumLength(GroupSettings.TitleMinimumLength)
            .MaximumLength(GroupSettings.TitleMaximumLength);

        RuleFor(command => command.UserIds)
            .Must(userIds => userIds.Except(dbContext.Users.ToList().Select(user => user.SourceId)).Count() == 0)
            .WithMessage("Some users don't exist.");
    }
}
