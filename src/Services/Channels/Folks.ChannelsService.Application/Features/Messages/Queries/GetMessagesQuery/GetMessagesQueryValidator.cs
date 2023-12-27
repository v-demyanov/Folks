using FluentValidation;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesQueryValidator : AbstractValidator<GetMessagesQuery>
{
    public GetMessagesQueryValidator(ChannelsServiceDbContext dbContext)
    {
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
    }
}
