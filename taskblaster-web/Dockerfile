# Base image for Node.js
FROM node:18-alpine

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json (or yarn.lock) from the taskblaster-web directory
COPY taskblaster-web/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code from the taskblaster-web directory
COPY taskblaster-web/ ./

# Expose the port the app runs on
EXPOSE 3000

# Specify the command to run the app
CMD ["npm", "run", "dev"]
