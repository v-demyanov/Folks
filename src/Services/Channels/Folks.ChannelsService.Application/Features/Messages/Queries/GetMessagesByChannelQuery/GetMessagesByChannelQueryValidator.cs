using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesByChannelQueryValidator : AbstractValidator<GetMessagesByChannelQuery>
{
    public GetMessagesByChannelQueryValidator(ChannelsServiceDbContext dbContext)
    {
        RuleFor(query => query.ChannelId)
            .NotEmpty();

        RuleFor(query => new ChannelMustExistCustomValidatorProperty
        {
            ChannelId = query.ChannelId,
            ChannelType = query.ChannelType
        }).ChannelMustExist(dbContext);
    }
}
