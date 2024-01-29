// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public AddUserCommandHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = this.mapper.Map<User>(request);

        this.dbContext.Add(user);
        this.dbContext.SaveChanges();

        return Task.CompletedTask;
    }
}
