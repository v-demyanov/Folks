# Folks
![Channels Service CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/ChannelsServiceDevCI.yml/badge.svg?branch=main)
![Identity Service CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/IdentityServiceDevCI.yml/badge.svg?branch=main)
![Api Gateway CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/ApiGatewayDevCI.yml/badge.svg?branch=main)
![Mobile Client CI (Dev)](https://github.com/v-demyanov/Folks/actions/workflows/MobileClientDevCI.yml/badge.svg?branch=main)

## Built with
- Frontend: [React Native][react-native-url], [Expo][expo-url], [React Native Paper][react-native-paper-url], [Redux Toolkit][redux-toolkit-url]
- Backend: [.NET 7][.net-url], [ASP.NET][asp.net-url], [MediatR][mediatr-url], [EF][ef-url], [Ocelot][ocelot-url], [MassTransit][masstransit-url]
- Persistence: [MongoDB][mongodb-url], [PostgreSQL][postgre-url]
- Messaging: [RabbitMQ][rabbitmq-url]
- Deploying: [Docker][docker-url], [Docker Compose][docker-compose-url]

## Project structure
| File / Folder | Description                                                                                               |
|---------------|-----------------------------------------------------------------------------------------------------------|
| ApiGateways   | Contains API Gateway applicatons source code                                                              |
| Common        | Contains shared class libraries source code for backend applications                                      |
| Frontend      | Contains frontend applications source code (for example mobile, web and etc.)                             |
| Services      | Contains backend microservices source code                                                                |
| docker-compose.yml </br> docker-compose.everride.yml | Contains instructions for managing and deploying docker containers |
| Makefile      | Contains commands for the application deployment (make deployment simple)                                 |
| .env.template | Contains template for creating `.env` file                                                                |

## Getting Started
This is the short guide for developers on how to deploy the application locally.

### Backend services deployment
To deploy the backend locally you have to install such dependencies as: [Docker][docker-url] and [Makefile][makefile-url].
- Create `.env` file in [src directory](./src) using [.env.template](./src/.env.template)
- Execute command `make up` from [src directory](./src)

### Mobile application deployment
To deploy the mobile application you can use cloud ([EAS][eas-url]) or local solution (it's required to install [XCode][xcode-url] or [Android Studio][android-studio-url]).
> [!IMPORTANT]
> Carry out instructions bellow from folder [Folks.MobileClient](./src/Frontend/Folks.MobileClient)
- If you have chosen locally application deployment, follow this [guide][expo-local-deployment-url]
- If you have chosen application deployment using ([EAS][eas-url]) follow steps below
  - Install EAS CLI using command `npm install -g eas-cli`
  - Contact the owner to add your expo account to the existing expo project
  - Create `eas.json` using [`eas.template.json`](./src/Frontend/Folks.MobileClient/eas.template.json)
  - Create `.env` using [`.env.template`](./src/Frontend/Folks.MobileClient/.env.template) and set environment variables depends on your deployed backend services
  - Follow steps 4 or 5 from the [guide][eas-deployment-url]
  - Execute command `npm run start`

## Migrations
### Add migrations - Identity Service
```
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c PersistedGrantDbContext -o PersistedGrantDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c ConfigurationDbContext -o ConfigurationDb
dotnet ef migrations add MigrationName --project Folks.IdentityService.Database.Migrations --startup-project Folks.IdentityService.Api -c IdentityServiceDbContext -o IdentityServiceDb
```

[react-native-url]: https://reactnative.dev/
[expo-url]: https://expo.dev/
[react-native-paper-url]: https://reactnativepaper.com/
[redux-toolkit-url]: https://redux-toolkit.js.org/
[.net-url]: https://dotnet.microsoft.com/en-us/
[asp.net-url]: https://dotnet.microsoft.com/en-us/apps/aspnet
[mediatr-url]: https://github.com/jbogard/MediatR
[ef-url]: https://learn.microsoft.com/en-us/ef/
[ocelot-url]: https://ocelot.readthedocs.io/
[masstransit-url]: https://masstransit.io/
[mongodb-url]: https://www.mongodb.com/
[postgre-url]: https://www.postgresql.org/
[rabbitmq-url]: https://rabbitmq.com/
[docker-url]: https://www.docker.com/
[docker-compose-url]: https://docs.docker.com/compose/
[makefile-url]: https://www.gnu.org/software/make/
[eas-url]: https://expo.dev/eas
[xcode-url]: https://developer.apple.com/xcode/
[android-studio-url]: https://developer.android.com/studio
[eas-deployment-url]: https://docs.expo.dev/develop/development-builds/create-a-build/
[expo-local-deployment-url]: https://docs.expo.dev/guides/local-app-development/
