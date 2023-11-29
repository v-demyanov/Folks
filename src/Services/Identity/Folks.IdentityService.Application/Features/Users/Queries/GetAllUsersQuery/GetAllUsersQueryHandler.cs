using MediatR;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Folks.IdentityService.Application.Features.Users.Dto;
using Folks.IdentityService.Infrastructure.Persistence;

namespace Folks.IdentityService.Application.Features.Users.Queries.GetAllUsersQuery;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IdentityServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IdentityServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }
}
