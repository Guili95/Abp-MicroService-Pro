#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gateways/Guili.BackendAdminAppGateway/Guili.BackendAdminAppGateway.csproj", "gateways/Guili.BackendAdminAppGateway/"]
COPY ["shared/Guili.Shared.Hosting.Gateways/Guili.Shared.Hosting.Gateways.csproj", "shared/Guili.Shared.Hosting.Gateways/"]
COPY ["shared/Guili.Shared.Hosting.AspNetCore/Guili.Shared.Hosting.AspNetCore.csproj", "shared/Guili.Shared.Hosting.AspNetCore/"]
COPY ["shared/Guili.Shared.Hosting/Guili.Shared.Hosting.csproj", "shared/Guili.Shared.Hosting/"]
RUN dotnet restore "gateways/Guili.BackendAdminAppGateway/Guili.BackendAdminAppGateway.csproj"
COPY . .
WORKDIR "/src/gateways/Guili.BackendAdminAppGateway"
RUN dotnet build "Guili.BackendAdminAppGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guili.BackendAdminAppGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guili.BackendAdminAppGateway.dll"]