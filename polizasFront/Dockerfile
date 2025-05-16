FROM node:18 as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build
RUN ls -la dist/polizas-front-app
RUN ls -la dist/polizas-front-app/browser

FROM nginx:alpine
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/polizas-front-app/browser/ /usr/share/nginx/html/
COPY nginx.conf /etc/nginx/conf.d/default.conf
RUN ls -la /usr/share/nginx/html
EXPOSE 8101
CMD ["nginx", "-g", "daemon off;"]