#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspNetCoreMicroservice/AspNetCoreMicroservice.csproj", "AspNetCoreMicroservice/"]
RUN dotnet restore "AspNetCoreMicroservice/AspNetCoreMicroservice.csproj"
COPY . .
WORKDIR "/src/AspNetCoreMicroservice"
RUN dotnet build "AspNetCoreMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreMicroservice.dll"]