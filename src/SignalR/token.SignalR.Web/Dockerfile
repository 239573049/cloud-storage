FROM mcr.microsoft.com/dotnet/aspnet:6.0.3-alpine3.15-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0.201-alpine3.15-amd64 AS build
WORKDIR /src
COPY ["src/SignalR/token.SignalR.Web/token.SignalR.Web.csproj", "src/SignalR/token.SignalR.Web/"]
COPY ["src/token.HttpApi/token.HttpApi.csproj", "src/token.HttpApi/"]
COPY ["src/token.Domain.Shared/token.Domain.Shared.csproj", "src/token.Domain.Shared/"]
RUN dotnet restore "src/SignalR/token.SignalR.Web/token.SignalR.Web.csproj"
COPY . .
WORKDIR "/src/src/SignalR/token.SignalR.Web"
RUN dotnet build "token.SignalR.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "token.SignalR.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "token.SignalR.Web.dll"]