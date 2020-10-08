FROM gcr.io/distroless/dotnet
WORKDIR /app
COPY src/TodoList.App.CommandHandler/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.CommandHandler.dll" ]
