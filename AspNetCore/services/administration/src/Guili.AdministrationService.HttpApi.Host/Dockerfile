#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["modules/identity/NuGet.Config", "modules/identity/"]
COPY ["services/administration/src/Guili.AdministrationService.HttpApi.Host/Guili.AdministrationService.HttpApi.Host.csproj", "services/administration/src/Guili.AdministrationService.HttpApi.Host/"]
COPY ["modules/identity/src/Guili.Identity.HttpApi.Client/Guili.Identity.HttpApi.Client.csproj", "modules/identity/src/Guili.Identity.HttpApi.Client/"]
COPY ["framework/Volo.Abp.Http.Client/Volo.Abp.Http.Client.csproj", "framework/Volo.Abp.Http.Client/"]
COPY ["modules/identity/src/Guili.Identity.Application.Contracts/Guili.Identity.Application.Contracts.csproj", "modules/identity/src/Guili.Identity.Application.Contracts/"]
COPY ["modules/identity/src/Guili.Identity.Domain.Shared/Guili.Identity.Domain.Shared.csproj", "modules/identity/src/Guili.Identity.Domain.Shared/"]
COPY ["services/administration/src/Guili.AdministrationService.EntityFrameworkCore/Guili.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/Guili.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain/Guili.AdministrationService.Domain.csproj", "services/administration/src/Guili.AdministrationService.Domain/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain.Shared/Guili.AdministrationService.Domain.Shared.csproj", "services/administration/src/Guili.AdministrationService.Domain.Shared/"]
COPY ["services/administration/src/Guili.AdministrationService.Application/Guili.AdministrationService.Application.csproj", "services/administration/src/Guili.AdministrationService.Application/"]
COPY ["services/administration/src/Guili.AdministrationService.Application.Contracts/Guili.AdministrationService.Application.Contracts.csproj", "services/administration/src/Guili.AdministrationService.Application.Contracts/"]
COPY ["services/administration/src/Guili.AdministrationService.HttpApi/Guili.AdministrationService.HttpApi.csproj", "services/administration/src/Guili.AdministrationService.HttpApi/"]
COPY ["shared/Guili.Shared.Hosting.Microservices/Guili.Shared.Hosting.Microservices.csproj", "shared/Guili.Shared.Hosting.Microservices/"]
COPY ["shared/Guili.Shared.Hosting.AspNetCore/Guili.Shared.Hosting.AspNetCore.csproj", "shared/Guili.Shared.Hosting.AspNetCore/"]
COPY ["shared/Guili.Shared.Hosting/Guili.Shared.Hosting.csproj", "shared/Guili.Shared.Hosting/"]
COPY ["shared/Guili.Shared.Localization/Guili.Shared.Localization.csproj", "shared/Guili.Shared.Localization/"]
RUN dotnet restore "services/administration/src/Guili.AdministrationService.HttpApi.Host/Guili.AdministrationService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/administration/src/Guili.AdministrationService.HttpApi.Host"
RUN dotnet build "Guili.AdministrationService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guili.AdministrationService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guili.AdministrationService.HttpApi.Host.dll"]