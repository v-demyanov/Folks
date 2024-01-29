// Copyright (c) v-demyanov. All rights reserved.

using MediatR;

namespace Folks.ChannelsService.Application.Common.Contracts;

public abstract class Notification : INotification
{
    required public IEnumerable<string> Recipients { get; init; }
}
