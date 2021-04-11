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

If you don't want to install cdk8s locally you can use this docker image

```bash
docker build -t deploy -f containers/Deploy.Dockerfile
```

Commands to build npm run

```bash
docker run -v ./deploy:/app:Z deploy npm run watch
docker run -v ./deploy:/app:Z deploy npm run compile
docker run -v ./deploy:/app:Z deploy npm run synth
kubectl apply -f ./deploy/dist/*.yaml
#sed "s/:latest/:0.1.0-configurable-app0001/" ./deploy/dist/todo.k8s.yaml | kubectl apply -f -
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





RET=`telnet localhost 6379 << EOF
XREAD BLOCK 0 STREAMS EventNet:Primary $
EOF`

echo $RET