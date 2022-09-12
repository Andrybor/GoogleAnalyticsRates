FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoogleAnalyticsAPI/GoogleAnalyticsAPI.csproj", "GoogleAnalyticsAPI/"]
RUN dotnet restore "GoogleAnalyticsAPI/GoogleAnalyticsAPI.csproj"
COPY . .
WORKDIR "/src/GoogleAnalyticsAPI"
RUN dotnet build "GoogleAnalyticsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleAnalyticsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleAnalyticsAPI.dll"]
