FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY src/src/TodoList.App.QueryHandler/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.QueryHandler.dll" ]
