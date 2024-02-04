// Copyright (c) v-demyanov. All rights reserved.

using Mongo2Go;
using MongoDB.Driver;

namespace Folks.ChannelsService.IntegrationTests.Fixtures;

public class DbFixture : IDisposable
{
    private bool disposed;

    public DbFixture()
    {
        this.Runner = MongoDbRunner.Start();
        this.Client = new MongoClient(this.Runner.ConnectionString);
    }

    public MongoDbRunner Runner { get; }

    public MongoClient Client { get; }

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
            this.Runner.Dispose();
        }

        this.disposed = true;
    }
}
