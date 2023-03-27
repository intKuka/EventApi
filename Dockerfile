FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5211

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventsApi/EventsApi.csproj", "./"]
RUN dotnet restore "./EventsApi.csproj"
COPY . .
RUN dotnet publish "EventsApi/EventsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EventsApi.dll"]