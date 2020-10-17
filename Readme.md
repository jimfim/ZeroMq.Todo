# Todo app

![.NET Core](https://github.com/jimfim/ZeroMq.Todo/workflows/.NET%20Core/badge.svg)

## build Projects

`
docker-compose build
`

## Infrastructure

``` bash
kind create cluster --config infra/kind.yaml --name todo
```

### Build Chart

```bash
docker build -t deploy -f containers/Deploy.Dockerfile
```

```bash
kubectl apply -f deploy/dist/*.yaml
```

### Watch Chart

```bash
docker run -it -v ./deploy:/app:Z deploy npm run watch
```

## Running The App

### Start Cluster

```bash
kind create cluster infra/kind.yaml
```

### Load Images

```bash
kind load docker-image docker.io/jimfim/query-proxy
kind load docker-image docker.io/jimfim/query-handler
kind load docker-image docker.io/jimfim/gateway
kind load docker-image docker.io/jimfim/command-proxy
kind load docker-image docker.io/jimfim/command-handler:latest
```

