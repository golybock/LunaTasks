FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Luna.Users.GrpcService/Luna.Users.GrpcService.csproj", "Luna.Users.GrpcService/"]
RUN dotnet restore "Luna.Users.GrpcService/Luna.Users.GrpcService.csproj"
COPY . .
WORKDIR "/src/Luna.Users.GrpcService"
RUN dotnet build "Luna.Users.GrpcService.csproj" -c docker -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Luna.Users.GrpcService.csproj" -c docker -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Luna.Users.GrpcService.dll"]
