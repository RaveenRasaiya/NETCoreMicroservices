#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Catalog.Microservice/Catalog.Microservice.csproj", "Catalog.Microservice/"]
RUN dotnet restore "Catalog.Microservice/Catalog.Microservice.csproj"
COPY . .
WORKDIR "/src/Catalog.Microservice"
RUN dotnet build "Catalog.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Microservice.dll"]
