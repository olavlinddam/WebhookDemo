FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebhookDemo.Api/WebhookDemo.Api.csproj", "WebhookDemo.Api/"]
RUN dotnet restore "WebhookDemo.Api/WebhookDemo.Api.csproj"
COPY . .
WORKDIR "/src/WebhookDemo.Api"
RUN dotnet build "WebhookDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebhookDemo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebhookDemo.Api.dll"]
