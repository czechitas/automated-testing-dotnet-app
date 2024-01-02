FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

EXPOSE 80
EXPOSE 443

ARG APP_PATH

WORKDIR /app

COPY ${APP_PATH} ./

ENTRYPOINT ["dotnet", "AutomatedTestingApp.dll"]
