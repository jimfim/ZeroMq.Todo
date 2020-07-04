FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY TodoList.App.CommandHandler/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.CommandHandler.dll" ]
