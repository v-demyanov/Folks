// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public CreateMessageCommandHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = this.mapper.Map<Message>(request);

        this.dbContext.Messages.Add(message);
        this.dbContext.SaveChanges();

        return Task.FromResult(this.mapper.Map<MessageDto>(message));
    }
}
