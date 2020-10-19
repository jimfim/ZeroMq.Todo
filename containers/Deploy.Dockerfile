FROM node:14.13.1-stretch-slim
RUN npm install -g cdk8s-cli typescript
#RUN npm run upgrade:next
WORKDIR /app
CMD [ "npm run compile" ]