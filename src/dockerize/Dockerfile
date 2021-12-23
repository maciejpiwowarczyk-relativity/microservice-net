FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY publish/ app/
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "AspNetCoreMicroservice.dll"]
