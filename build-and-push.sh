#dotnet publish -c Release ./src/
podman-compose build
# podman-compose cna't push to insecure registries
podman push --tls-verify=false localhost:5000/gateway:latest

podman push --tls-verify=false localhost:5000/query-handler:latest
podman push --tls-verify=false localhost:5000/query-proxy:latest

podman push --tls-verify=false localhost:5000/command-handler:latest
podman push --tls-verify=false localhost:5000/command-proxy:latest
