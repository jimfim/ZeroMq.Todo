FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY Gateway/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "Gateway.dll" ]
