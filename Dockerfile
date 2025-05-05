# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# install Node.js & npm (Debian-based)
RUN apt-get update \
 && apt-get install -y curl gnupg \
 # bring in NodeSource signing key
 && curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
 && apt-get install -y nodejs \
 && rm -rf /var/lib/apt/lists/*

# Copy both projects
COPY ./ECommerceAPI/ ./ECommerceAPI/
COPY ./testecommerceFrontend/ ./testecommerceFrontend/

# Build the frontend
WORKDIR /src/testecommerceFrontend
RUN npm install && npm run build

# Copy built frontend into the API's wwwroot
WORKDIR /src/ECommerceAPI
RUN mkdir -p wwwroot && \
    cp -r ../testecommerceFrontend/dist/* wwwroot/

# Restore & publish the API
RUN dotnet restore ECommerceAPI.csproj
RUN dotnet publish ECommerceAPI.csproj -c Release -o /app/publish

# run time
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published output
COPY --from=build /app/publish .

# Tell ASP.NET Core to listen on all interfaces, port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Expose the Fly internal port
EXPOSE 8080

# Start the API (which will also serve your index.html from wwwroot)
ENTRYPOINT ["dotnet", "ECommerceAPI.dll"]
