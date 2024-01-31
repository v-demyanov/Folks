# Folks
![Channels Service CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/ChannelsServiceDevCI.yml/badge.svg?branch=main)
![Identity Service CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/IdentityServiceDevCI.yml/badge.svg?branch=main)
![Api Gateway CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/ApiGatewayDevCI.yml/badge.svg?branch=main)
![Mobile Client CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/MobileClientDevCI.yml/badge.svg?branch=main)

### Migrations
### Add migrations - Identity Service
```
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c PersistedGrantDbContext -o PersistedGrantDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c ConfigurationDbContext -o ConfigurationDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c IdentityServiceDbContext -o IdentityServiceDb
```
