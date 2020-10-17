FROM node:14.13.1-stretch-slim
RUN npm install -g cdk8s-cli typescript
WORKDIR /app