FROM node:26-alpine

WORKDIR /app

COPY package*.json ./
RUN npm ci --legacy-peer-deps

COPY . .

RUN npx ng build --configuration production

RUN npm install -g serve

EXPOSE 4200

CMD ["serve", "-s", "dist/my-app/browser", "-l", "4200"]