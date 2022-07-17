FROM mcr.microsoft.com/dotnet/aspnet:6.0.3-alpine3.15-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0.201-alpine3.15-amd64 AS build
WORKDIR /src
COPY ["src/CloudStorage.HttpApi.Host/CloudStorage.HttpApi.Host.csproj", "src/CloudStorage.HttpApi.Host/"]
COPY ["src/CloudStorage.Application/CloudStorage.Application.csproj", "src/CloudStorage.Application/"]
COPY ["src/CloudStorage.Application.Contracts/CloudStorage.Application.Contracts.csproj", "src/CloudStorage.Application.Contracts/"]
COPY ["src/CloudStorage.Domain.Shared/CloudStorage.Domain.Shared.csproj", "src/CloudStorage.Domain.Shared/"]
COPY ["src/CloudStorage.EntityFrameworkCore/CloudStorage.EntityFrameworkCore.csproj", "src/CloudStorage.EntityFrameworkCore/"]
COPY ["src/CloudStorage.Domain/CloudStorage.Domain.csproj", "src/CloudStorage.Domain/"]
COPY ["src/CloudStorage.HttpApi/CloudStorage.HttpApi.csproj", "src/CloudStorage.HttpApi/"]
RUN dotnet restore "src/CloudStorage.HttpApi.Host/CloudStorage.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/CloudStorage.HttpApi.Host"
RUN dotnet build "CloudStorage.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudStorage.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudStorage.HttpApi.Host.dll"]