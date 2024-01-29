// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Application.Exceptions;

public class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException()
        : base($"Authentication failed.")
    {
    }
}
