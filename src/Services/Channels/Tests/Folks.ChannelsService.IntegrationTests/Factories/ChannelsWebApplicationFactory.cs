// Copyright (c) v-demyanov. All rights reserved.

using System.Data.Common;

using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.IntegrationTests.Constants;
using Folks.ChannelsService.IntegrationTests.Fixtures;
using Folks.ChannelsService.IntegrationTests.Handlers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Folks.ChannelsService.IntegrationTests.Factories;

public class ChannelsWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbFixture dbFixture;

    public ChannelsWebApplicationFactory(DbFixture dbFixture)
    {
        this.dbFixture = dbFixture ?? throw new ArgumentNullException(nameof(dbFixture));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(defaultScheme: TestJwtConstants.TestJwtScheme)
                .AddScheme<JwtBearerOptions, TestJwtBearerHandler>(TestJwtConstants.TestJwtScheme, options => { });

            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ChannelsServiceDbContext>));
            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
            if (dbConnectionDescriptor is not null)
            {
                services.Remove(dbConnectionDescriptor);
            }

            services.AddDbContext<ChannelsServiceDbContext>(options =>
                options.UseMongoDB(this.dbFixture.Client, "ChannelsDb"));
        });

        base.ConfigureWebHost(builder);
    }
}
