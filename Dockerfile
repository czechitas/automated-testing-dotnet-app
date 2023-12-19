EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

ARG APP_PATH

WORKDIR /app

COPY ${APP_PATH} ./

ENTRYPOINT ["dotnet", "AutomatedTestingApp.dll"]
