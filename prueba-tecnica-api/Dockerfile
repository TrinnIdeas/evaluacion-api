#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["prueba-tecnica-api/prueba-tecnica-api.csproj", "prueba-tecnica-api/"]
RUN dotnet restore "prueba-tecnica-api/prueba-tecnica-api.csproj"
COPY . .
WORKDIR "/src/prueba-tecnica-api"
RUN dotnet build "prueba-tecnica-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "prueba-tecnica-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "prueba-tecnica-api.dll"]