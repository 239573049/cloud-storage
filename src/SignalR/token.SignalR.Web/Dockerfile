FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SignalR/token.SignalR.Web/token.SignalR.Web.csproj", "token.SignalR.Web/"]
RUN dotnet restore "src/SignalR/token.SignalR.Web/token.SignalR.Web.csproj"
COPY . .
WORKDIR "/src/token.SignalR.Web"
RUN dotnet build "token.SignalR.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "token.SignalR.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "token.SignalR.Web.dll"]
