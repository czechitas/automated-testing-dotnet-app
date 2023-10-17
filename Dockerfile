FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AutomatedTestingApp/AutomatedTestingApp/AutomatedTestingApp.csproj", "AutomatedTestingApp/"]
RUN dotnet restore "AutomatedTestingApp/AutomatedTestingApp.csproj"
COPY . .
WORKDIR "/src/AutomatedTestingApp"
RUN dotnet build "AutomatedTestingApp.csproj" -c Release

FROM build AS publish
RUN dotnet publish "AutomatedTestingApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutomatedTestingApp.dll"]
