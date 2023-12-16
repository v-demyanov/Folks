using AutoMapper;

using MediatR;

using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddUserCommandHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        _dbContext.Add(user);
        _dbContext.SaveChanges();

        return Task.CompletedTask;
    }
}
