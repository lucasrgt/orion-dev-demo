FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrionDev.ClientAPI/OrionDev.ClientAPI.csproj", "OrionDev.ClientAPI/"]
RUN dotnet restore "OrionDev.ClientAPI/OrionDev.ClientAPI.csproj"
COPY . .
WORKDIR "/src/OrionDev.ClientAPI"
RUN dotnet build "OrionDev.ClientAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrionDev.ClientAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrionDev.ClientAPI.dll"]
