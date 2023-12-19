using FluentValidation;

using Folks.ChannelsService.Application.Features.Channels.Enums;
using Folks.ChannelsService.Domain.Constants;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator(ChatServiceDbContext dbContext)
    {
        RuleFor(query => query.OwnerId)
            .NotEmpty();

        RuleFor(query => query.OwnerId)
            .Must(ownerId => dbContext.Users.Any(user => user.SourceId == ownerId))
            .WithMessage(query => $"The user with id=\"{query.OwnerId}\" doesn't exist.");

        RuleFor(query => query.ChannelId)
            .NotEmpty();

        RuleFor(query => query.ChannelId)
            .Must((command, channelId) =>
                command.ChannelType switch
                {
                    ChannelType.Chat => dbContext.Chats.Any(chat => chat.Id.ToString() == channelId),
                    ChannelType.Group => dbContext.Groups.Any(group => group.Id.ToString() == channelId),
                    _ => false,
                })
            .WithMessage(query => $"The channel with id=\"{query.ChannelId}\" doesn't exist.");

        RuleFor(command => command.Content)
            .NotEmpty()
            .MaximumLength(MessageSettings.ContentMaximumLength);
    }
}
