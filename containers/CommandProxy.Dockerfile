FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY src/TodoList.App.CommandProxy/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.CommandProxy.dll" ]
