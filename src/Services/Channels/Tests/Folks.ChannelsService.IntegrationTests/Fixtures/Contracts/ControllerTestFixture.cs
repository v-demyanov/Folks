// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.IntegrationTests.Factories;
using Folks.ChannelsService.IntegrationTests.Factories.Contracts;
using Folks.Tests.Common.Contracts;

namespace Folks.ChannelsService.IntegrationTests.Fixtures.Contracts;

public abstract class ControllerTestFixture<TControllerClient> : IDisposable
    where TControllerClient : BaseControllerClient
{
    private bool disposed;

    protected ControllerTestFixture()
    {
        this.DbFixture = new DbFixture();
        this.WebApplicationFactory = new ChannelsWebApplicationFactory(this.DbFixture);
    }

    ~ControllerTestFixture()
    {
        this.Dispose(false);
    }

    public DbFixture DbFixture { get; }

    public ChannelsWebApplicationFactory WebApplicationFactory { get; }

    public abstract ControllerClientFactory<TControllerClient> ControllerClientFactory { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            this.WebApplicationFactory.Dispose();
            this.DbFixture.Dispose();
        }

        this.disposed = true;
    }
}
