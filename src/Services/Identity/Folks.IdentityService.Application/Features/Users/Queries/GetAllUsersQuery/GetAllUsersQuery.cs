using MediatR;

using Folks.IdentityService.Application.Features.Users.Dto;

namespace Folks.IdentityService.Application.Features.Users.Queries.GetAllUsersQuery;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
