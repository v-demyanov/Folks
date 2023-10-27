# Folks

### Migrations
### Add migration (Identity Service)
```
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c PersistedGrantDbContext -o PersistedGrantDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c ConfigurationDbContext -o ConfigurationDb
```
