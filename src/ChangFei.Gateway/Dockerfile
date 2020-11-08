#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ChangFei.Gateway/ChangFei.Gateway.csproj", "ChangFei.Gateway/"]
RUN dotnet restore "ChangFei.Gateway/ChangFei.Gateway.csproj"
COPY . .
WORKDIR "/src/ChangFei.Gateway"
RUN dotnet build "ChangFei.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChangFei.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChangFei.Gateway.dll"]