
FROM node:alpine AS BUILDER

WORKDIR /app

COPY . .

RUN npm install

RUN npm run build  --configuration production

FROM nginx:alpine

COPY --from=builder /app/dist/payment-app/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD [ "nginx", "-g", "daemon off;" ]