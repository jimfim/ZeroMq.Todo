FROM gcr.io/distroless/dotnet
WORKDIR /app
COPY TodoList.App.QueryProxy/bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "TodoList.App.QueryProxy.dll" ]
