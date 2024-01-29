// Copyright (c) v-demyanov. All rights reserved.

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
        this.RuleFor(command => command.OwnerId)
            .NotEmpty();

        this.RuleFor(command => command.OwnerId)
            .UserMustExist(dbContext);

        this.RuleFor(command => command.ChannelId)
            .NotEmpty();

        this.RuleFor(command => new ChannelMustExistCustomValidatorProperty
        {
            ChannelId = command.ChannelId,
            ChannelType = command.ChannelType,
        }).ChannelMustExist(dbContext);

        this.RuleFor(command => command.Content)
            .NotEmpty()
            .MaximumLength(MessageSettings.ContentMaximumLength)
            .When(command => command.Type == MessageType.Text);
    }
}
