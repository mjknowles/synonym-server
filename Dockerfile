FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore && dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0.4 AS prod
EXPOSE  5000
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "synonym-server.dll", "--urls", "http://localhost:5000"]
