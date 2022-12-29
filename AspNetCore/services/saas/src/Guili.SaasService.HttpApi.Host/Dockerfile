#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/src/Guili.SaasService.HttpApi.Host/Guili.SaasService.HttpApi.Host.csproj", "services/saas/src/Guili.SaasService.HttpApi.Host/"]
COPY ["shared/Guili.Shared.Hosting.Microservices/Guili.Shared.Hosting.Microservices.csproj", "shared/Guili.Shared.Hosting.Microservices/"]
COPY ["services/administration/src/Guili.AdministrationService.EntityFrameworkCore/Guili.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/Guili.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain/Guili.AdministrationService.Domain.csproj", "services/administration/src/Guili.AdministrationService.Domain/"]
COPY ["services/administration/src/Guili.AdministrationService.Domain.Shared/Guili.AdministrationService.Domain.Shared.csproj", "services/administration/src/Guili.AdministrationService.Domain.Shared/"]
COPY ["shared/Guili.Shared.Hosting.AspNetCore/Guili.Shared.Hosting.AspNetCore.csproj", "shared/Guili.Shared.Hosting.AspNetCore/"]
COPY ["shared/Guili.Shared.Hosting/Guili.Shared.Hosting.csproj", "shared/Guili.Shared.Hosting/"]
COPY ["shared/Guili.Shared.Localization/Guili.Shared.Localization.csproj", "shared/Guili.Shared.Localization/"]
COPY ["services/saas/src/Guili.SaasService.Application/Guili.SaasService.Application.csproj", "services/saas/src/Guili.SaasService.Application/"]
COPY ["services/saas/src/Guili.SaasService.Domain/Guili.SaasService.Domain.csproj", "services/saas/src/Guili.SaasService.Domain/"]
COPY ["services/saas/src/Guili.SaasService.Domain.Shared/Guili.SaasService.Domain.Shared.csproj", "services/saas/src/Guili.SaasService.Domain.Shared/"]
COPY ["services/saas/src/Guili.SaasService.Application.Contracts/Guili.SaasService.Application.Contracts.csproj", "services/saas/src/Guili.SaasService.Application.Contracts/"]
COPY ["services/saas/src/Guili.SaasService.EntityFrameworkCore/Guili.SaasService.EntityFrameworkCore.csproj", "services/saas/src/Guili.SaasService.EntityFrameworkCore/"]
COPY ["services/saas/src/Guili.SaasService.HttpApi/Guili.SaasService.HttpApi.csproj", "services/saas/src/Guili.SaasService.HttpApi/"]
RUN dotnet restore "services/saas/src/Guili.SaasService.HttpApi.Host/Guili.SaasService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/src/Guili.SaasService.HttpApi.Host"
RUN dotnet build "Guili.SaasService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guili.SaasService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guili.SaasService.HttpApi.Host.dll"]