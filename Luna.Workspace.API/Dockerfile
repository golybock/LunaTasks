FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Luna.Workspace.API/Luna.Workspace.API.csproj", "Luna.Workspace.API/"]
RUN dotnet restore "Luna.Workspace.API/Luna.Workspace.API.csproj"
COPY . .
WORKDIR "/src/Luna.Workspace.API"
RUN dotnet build "Luna.Workspace.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Luna.Workspace.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Luna.Workspace.API.dll"]
