#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Folks.IdentityService.Api/Folks.IdentityService.Api.csproj", "Services/Identity/Folks.IdentityService.Api/"]
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