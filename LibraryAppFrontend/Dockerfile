FROM node:latest as build


WORKDIR /usr/local/app

COPY ./ /usr/local/app/

RUN npm install

RUN npm run build

#NGINX

FROM nginx:latest

COPY angulardefault.conf /etc/nginx/conf.d/default.conf

COPY --from=build /usr/local/app/dist/library-app-frontend /usr/share/nginx/html

EXPOSE 80