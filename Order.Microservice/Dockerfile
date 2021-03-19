#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Order.Microservice/Order.Microservice.csproj", "Order.Microservice/"]
RUN dotnet restore "Order.Microservice/Order.Microservice.csproj"
COPY . .
WORKDIR "/src/Order.Microservice"
RUN dotnet build "Order.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Microservice.dll"]
