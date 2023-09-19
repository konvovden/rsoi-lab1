FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

EXPOSE 80

COPY PersonServiceBuild/ .

ENTRYPOINT ["dotnet", "PersonService.Server.dll"]

