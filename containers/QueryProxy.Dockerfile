FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY src/TodoList.App.QueryProxy/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.QueryProxy.dll" ]
