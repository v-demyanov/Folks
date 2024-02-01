// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Groups.Common.Dto;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;

public class GetGroupQuery : IRequest<GroupDto>
{
    required public string GroupId { get; init; }

    required public string CurrentUserId { get; init; }
}
