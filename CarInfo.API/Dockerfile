#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarInfo.API.csproj", "."]
COPY ["../CarInfo.Domain/CarInfo.Domain.csproj", "../CarInfo.Domain/"]
COPY ["../CarInfo.Infraestructure/CarInfo.Infraestructure.csproj", "../CarInfo.Infraestructure/"]
RUN dotnet restore "./CarInfo.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CarInfo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarInfo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarInfo.API.dll"]