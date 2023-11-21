using AutoMapper;

using MediatR;

using Folks.ChatService.Infrastructure.Persistence;
using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Features.Users.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddUserCommandHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}
