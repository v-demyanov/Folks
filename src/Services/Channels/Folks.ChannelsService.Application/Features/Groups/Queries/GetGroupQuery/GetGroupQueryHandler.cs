// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public GetGroupQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = this.dbContext.Groups.GetById(request.GroupId);
        var groupDto = this.mapper.Map<GroupDto>(group);

        return Task.FromResult(groupDto);
    }
}
