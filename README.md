# Folks
![Channels Service Dev CI](https://github.com/v-demyanov/Folks/actions/workflows/ChannelsServiceDevelopment.yml/badge.svg?branch=main)
![Identity Service Dev CI](https://github.com/v-demyanov/Folks/actions/workflows/IdentityServiceDevelopment.yml/badge.svg?branch=main)
![Api Gateway Dev CI](https://github.com/v-demyanov/Folks/actions/workflows/ApiGatewayDevelopment.yml/badge.svg?branch=main)
![Mobile Client Dev CI](https://github.com/v-demyanov/Folks/actions/workflows/MobileClientDevelopment.yml/badge.svg?branch=main)

### Migrations
### Add migrations - Identity Service
```
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c PersistedGrantDbContext -o PersistedGrantDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c ConfigurationDbContext -o ConfigurationDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c IdentityServiceDbContext -o IdentityServiceDb
```
