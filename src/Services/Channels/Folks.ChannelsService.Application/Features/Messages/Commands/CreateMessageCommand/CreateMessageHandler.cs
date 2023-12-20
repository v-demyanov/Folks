﻿using MediatR;

using AutoMapper;

using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, MessageDto>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateMessageHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<Message>(request);

        _dbContext.Messages.Add(message);
        _dbContext.SaveChanges();

        return Task.FromResult(_mapper.Map<MessageDto>(message));
    }
}