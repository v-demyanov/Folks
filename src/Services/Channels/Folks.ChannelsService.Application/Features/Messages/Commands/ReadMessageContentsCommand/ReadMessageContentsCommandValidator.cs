using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommandValidator : AbstractValidator<ReadMessageContentsCommand>
{
    public ReadMessageContentsCommandValidator(ChannelsServiceDbContext dbContext)
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.UserId)
            .UserMustExist(dbContext);

        RuleFor(command => command.ChannelId)
            .NotEmpty();

        RuleFor(command => new ChannelMustExistCustomValidatorProperty 
        { 
            ChannelId = command.ChannelId, 
            ChannelType = command.ChannelType 
        }).ChannelMustExist(dbContext);

        RuleFor(command => command.MessageIds)
            .Must(messageIds => messageIds.Except(dbContext.Messages
                .AsEnumerable()
                .Select(message => message.Id.ToString())).Count() == 0)
            .WithMessage("Some messages don't exist.");
    }
}