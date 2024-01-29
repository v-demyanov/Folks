// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Domain.Common.Abstractions;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
}
