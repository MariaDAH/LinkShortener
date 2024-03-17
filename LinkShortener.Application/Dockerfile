FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LinkShortShare.Application/LinkShortShare.Application.csproj", "LinkShortShare.Application/"]
RUN dotnet restore "LinkShortShare.Application/LinkShortShare.Application.csproj"
COPY . .
WORKDIR "/src/LinkShortShare"
RUN dotnet build "LinkShortShare.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LinkShortShare.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinkShortShare.Application.dll"]
