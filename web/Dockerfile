FROM node:alpine as build
COPY . /app 
WORKDIR /app
ENV VUE_APP_ENV=production
RUN yarn
RUN yarn build

FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
