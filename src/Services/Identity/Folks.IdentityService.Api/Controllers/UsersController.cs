using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using System.Net;

using static Duende.IdentityServer.IdentityServerConstants;

using Folks.IdentityService.Api.Constants;
using Folks.IdentityService.Application.Features.Users.Queries.GetAllUsersQuery;
using Folks.IdentityService.Application.Features.Users.Dto;

namespace Folks.IdentityService.Api.Controllers;

[ApiController]
[Authorize(LocalApi.PolicyName)]
[Route($"{ApiRoutePatterns.BaseRoute}[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);

        return Ok(users);
    }
}
