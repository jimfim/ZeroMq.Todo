# Todo app
![.NET Core](https://github.com/jimfim/ZeroMq.Todo/workflows/.NET%20Core/badge.svg)


# build images
`
dotnet build -c Release src/
`

`
docker-compose build
`

# Infrastructure

` bash
kind create cluster --config infra/kind.yaml --name todo
`

## load images

`bash
kind load docker-image docker.io/jimfim/query-proxy 
kind load docker-image docker.io/jimfim/query-handler
kind load docker-image docker.io/jimfim/gateway
kind load docker-image docker.io/jimfim/command-proxy
kind load docker-image docker.io/jimfim/command-handler:latest
`