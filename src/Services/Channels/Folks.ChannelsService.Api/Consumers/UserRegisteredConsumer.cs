using MassTransit;

using MediatR;

using AutoMapper;

using Folks.EventBus.Messages.IdentityService;
using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;

namespace Folks.ChannelsService.Api.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserRegisteredConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var addUserCommand = _mapper.Map<AddUserCommand>(context.Message);
        await _mediator.Send(addUserCommand);
    }
}
