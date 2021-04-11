podman run -v ./deploy:/app:Z deploy npm run compile
podman run -v ./deploy:/app:Z deploy npm run synth


kubectl apply -f ./deploy/dist/*.yaml