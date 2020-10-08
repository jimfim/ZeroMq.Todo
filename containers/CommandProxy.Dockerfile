FROM gcr.io/distroless/dotnet
WORKDIR /app
COPY src/TodoList.App.CommandProxy/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.CommandProxy.dll" ]
