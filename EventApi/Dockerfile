﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventApi/EventApi.csproj", "EventApi/"]
RUN dotnet restore "EventApi/EventApi.csproj"
COPY . .
WORKDIR "/src/EventApi"
RUN dotnet build "EventApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventApi.dll"]
