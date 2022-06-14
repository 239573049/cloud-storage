#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/token.Web/token.Web.csproj", "src/token.Web/"]
COPY ["src/token.Application/token.Application.csproj", "src/token.Application/"]
COPY ["src/token.EntityFrameworkCore/token.EntityFrameworkCore.csproj", "src/token.EntityFrameworkCore/"]
COPY ["src/token.Domain/token.Domain.csproj", "src/token.Domain/"]
COPY ["src/token.Application.Contracts/token.Application.Contracts.csproj", "src/token.Application.Contracts/"]
COPY ["src/token.HttpApi/token.HttpApi.csproj", "src/token.HttpApi/"]
COPY ["src/token.Domain.Shared/token.Domain.Shared.csproj", "src/token.Domain.Shared/"]
RUN dotnet restore "src/token.Web/token.Web.csproj"
COPY . .
WORKDIR "/src/src/token.Web"
RUN dotnet build "token.Web.csproj" -c Release --os linux -o /app/build

FROM build AS publish
RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
RUN dotnet publish "token.Web.csproj" -c Release --os linux -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "token.Web.dll"]