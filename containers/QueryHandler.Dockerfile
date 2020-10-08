FROM gcr.io/distroless/dotnet
WORKDIR /app
COPY src/src/TodoList.App.QueryHandler/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.QueryHandler.dll" ]
