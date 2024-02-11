FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Folks.IdentityService.Api/Folks.IdentityService.Api.csproj", "Services/Identity/Folks.IdentityService.Api/"]
COPY ["Services/Identity/Folks.IdentityService.Application/Folks.IdentityService.Application.csproj", "Services/Identity/Folks.IdentityService.Application/"]
COPY ["Services/Identity/Folks.IdentityService.Domain/Folks.IdentityService.Domain.csproj", "Services/Identity/Folks.IdentityService.Domain/"]
COPY ["Services/Identity/Folks.IdentityService.Database.Migrations/Folks.IdentityService.Database.Migrations.csproj", "Services/Identity/Folks.IdentityService.Database.Migrations/"]
COPY ["Services/Identity/Folks.IdentityService.Infrastructure/Folks.IdentityService.Infrastructure.csproj", "Services/Identity/Folks.IdentityService.Infrastructure/"]
COPY ["Common/Folks.EventBus.Messages/Folks.EventBus.Messages.csproj", "Common/Folks.EventBus.Messages/"]
RUN dotnet restore "Services/Identity/Folks.IdentityService.Api/Folks.IdentityService.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Folks.IdentityService.Api"
RUN dotnet build "Folks.IdentityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Folks.IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Folks.IdentityService.Api.dll"]