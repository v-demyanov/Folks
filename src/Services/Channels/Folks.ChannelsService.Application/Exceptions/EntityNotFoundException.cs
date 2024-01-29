// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
