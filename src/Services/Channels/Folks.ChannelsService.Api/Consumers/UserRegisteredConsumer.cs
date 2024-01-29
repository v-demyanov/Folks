// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;
using Folks.EventBus.Messages.IdentityService;

using MassTransit;
using MediatR;

namespace Folks.ChannelsService.Api.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public UserRegisteredConsumer(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var addUserCommand = this.mapper.Map<AddUserCommand>(context.Message);
        await this.mediator.Send(addUserCommand);
    }
}
