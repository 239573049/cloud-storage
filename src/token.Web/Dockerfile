FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/token.Web/token.Web.csproj", "token.Web/"]
COPY ["src/token.HttpApi/token.HttpApi.csproj", "token.HttpApi/"]
COPY ["src/token.Domain.Shared/token.Domain.Shared.csproj", "token.Domain.Shared/"]
COPY ["src/token.Application.Contracts/token.Application.Contracts.csproj", "token.Application.Contracts/"]
COPY ["src/token.Application/token.Application.csproj", "token.Application/"]
COPY ["src/token.EntityFrameworkCore/token.EntityFrameworkCore.csproj", "token.EntityFrameworkCore/"]
COPY ["src/token.Domain/token.Domain.csproj", "token.Domain/"]
RUN dotnet restore "src/token.Web/token.Web.csproj"
COPY . .
WORKDIR "/src/token.Web"
RUN dotnet build "token.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "token.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "token.Web.dll"]
