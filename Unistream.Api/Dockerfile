# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Unistream.Api/Unistream.Api.csproj", "Unistream.Api/"]
COPY ["Unistream.Application/Unistream.Application.csproj", "Unistream.Application/"]
COPY ["Unistream.Infrastructure/Unistream.Infrastructure.csproj", "Unistream.Infrastructure/"]
COPY ["Unistream.Domain/Unistream.Domain.csproj", "Unistream.Domain/"]
RUN dotnet restore "./Unistream.Api/Unistream.Api.csproj"
COPY . .
WORKDIR "/src/Unistream.Api"
RUN dotnet build "./Unistream.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Unistream.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unistream.Api.dll"]