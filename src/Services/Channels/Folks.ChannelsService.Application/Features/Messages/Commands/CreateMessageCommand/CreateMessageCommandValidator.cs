using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Domain.Common.Constants;
using Folks.ChannelsService.Domain.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator(ChannelsServiceDbContext dbContext)
    {
        RuleFor(command => command.OwnerId)
            .NotEmpty();

        RuleFor(command => command.OwnerId)
            .UserMustExist(dbContext);

        RuleFor(command => command.ChannelId)
            .NotEmpty();

        RuleFor(command => new ChannelMustExistCustomValidatorProperty
        {
            ChannelId = command.ChannelId,
            ChannelType = command.ChannelType
        }).ChannelMustExist(dbContext);

        RuleFor(command => command.Content)
            .NotEmpty()
            .MaximumLength(MessageSettings.ContentMaximumLength)
            .When(command => command.Type == MessageType.Text);
    }
}
