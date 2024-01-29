// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommandValidator : AbstractValidator<ReadMessageContentsCommand>
{
    public ReadMessageContentsCommandValidator(ChannelsServiceDbContext dbContext)
    {
        this.RuleFor(command => command.UserId)
            .NotEmpty();

        this.RuleFor(command => command.UserId)
            .UserMustExist(dbContext);

        this.RuleFor(command => command.ChannelId)
            .NotEmpty();

        this.RuleFor(command => new ChannelMustExistCustomValidatorProperty
        {
            ChannelId = command.ChannelId,
            ChannelType = command.ChannelType,
        }).ChannelMustExist(dbContext);

        this.RuleFor(command => command.MessageIds)
            .Must(messageIds => messageIds.Except(dbContext.Messages
                .AsEnumerable()
                .Select(message => message.Id.ToString())).Count() == 0)
            .WithMessage("Some messages don't exist.");
    }
}