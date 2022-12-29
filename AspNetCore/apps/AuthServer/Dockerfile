#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["modules/identity/NuGet.Config", "modules/identity/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["apps/AuthServer/AuthServer.csproj", "apps/AuthServer/"]
COPY ["services/identity/src/Guili.IdentityService.EntityFrameworkCore/Guili.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/Guili.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/Guili.IdentityService.Domain/Guili.IdentityService.Domain.csproj", "services/identity/src/Guili.IdentityService.Domain/"]
COPY ["modules/identity/src/Guili.Identity.Domain/Guili.Identity.Domain.csproj", "modules/identity/src/Guili.Identity.Domain/"]
COPY ["modules/identity/src/Guili.Identity.Domain.Shared/Guili.Identity.Domain.Shared.csproj", "modules/identity/src/Guili.Identity.Domain.Shared/"]
COPY ["services/identity/src/Guili.IdentityService.Domain.Shared/Guili.IdentityService.Domain.Shared.csproj", "services/identity/src/Guili.IdentityService.Domain.Shared/"]
COPY ["shared/Guili.Shared.Hosting.Microservices/Guili.Shared.Hosting.Microservices.csproj", "shared/Guili.Shared.Hosting.Microservices/"]
COPY ["services/administration/src/Guili.AdministrationService.EntityFrameworkCore/Guili.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/Guili.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain/Guili.AdministrationService.Domain.csproj", "services/administration/src/Guili.AdministrationService.Domain/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain.Shared/Guili.AdministrationService.Domain.Shared.csproj", "services/administration/src/Guili.AdministrationService.Domain.Shared/"]
COPY ["shared/Guili.Shared.Hosting.AspNetCore/Guili.Shared.Hosting.AspNetCore.csproj", "shared/Guili.Shared.Hosting.AspNetCore/"]
COPY ["shared/Guili.Shared.Hosting/Guili.Shared.Hosting.csproj", "shared/Guili.Shared.Hosting/"]
COPY ["shared/Guili.Shared.Localization/Guili.Shared.Localization.csproj", "shared/Guili.Shared.Localization/"]
RUN dotnet restore "apps/AuthServer/AuthServer.csproj"
COPY . .
WORKDIR "/src/apps/AuthServer"
RUN dotnet build "AuthServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthServer.dll"]