FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:7.0.4-alpine3.17 AS prod
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "synonym-server.dll"]
