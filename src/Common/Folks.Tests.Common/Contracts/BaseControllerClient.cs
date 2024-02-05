// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.Tests.Common.Contracts;

public abstract class BaseControllerClient
{
    protected BaseControllerClient(HttpClient httpClient, string baseRoute)
    {
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.BaseRoute = baseRoute ?? throw new ArgumentNullException(nameof(baseRoute));
    }

    protected HttpClient HttpClient { get; }

    protected string BaseRoute { get; }
}
