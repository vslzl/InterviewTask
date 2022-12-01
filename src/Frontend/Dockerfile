#stage 1
FROM node:latest as node
WORKDIR /app
COPY ["src/Frontend/", "."]
# COPY ["src/default.conf", "."]
RUN npm install
RUN npm run build
FROM nginx:alpine
COPY --from=node /app/dist/frontend /usr/share/nginx/html
COPY --from=node /app/default.conf /etc/nginx/conf.d/default.conf

# When the container starts, replace the env.js with values from environment variables
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]