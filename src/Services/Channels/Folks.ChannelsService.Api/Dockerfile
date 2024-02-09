FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Channels/Folks.ChannelsService.Api/Folks.ChannelsService.Api.csproj", "Services/Channels/Folks.ChannelsService.Api/"]
COPY ["Services/Channels/Folks.ChannelsService.Application/Folks.ChannelsService.Application.csproj", "Services/Channels/Folks.ChannelsService.Application/"]
COPY ["Services/Channels/Folks.ChannelsService.Infrastructure/Folks.ChannelsService.Infrastructure.csproj", "Services/Channels/Folks.ChannelsService.Infrastructure/"]
COPY ["Services/Channels/Folks.ChannelsService.Domain/Folks.ChannelsService.Domain.csproj", "Services/Channels/Folks.ChannelsService.Domain/"]
COPY ["Common/Folks.EventBus.Messages/Folks.EventBus.Messages.csproj", "Common/Folks.EventBus.Messages/"]
RUN dotnet restore "Services/Channels/Folks.ChannelsService.Api/Folks.ChannelsService.Api.csproj"
COPY . .
WORKDIR "/src/Services/Channels/Folks.ChannelsService.Api"
RUN dotnet build "Folks.ChannelsService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Folks.ChannelsService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Folks.ChannelsService.Api.dll"]