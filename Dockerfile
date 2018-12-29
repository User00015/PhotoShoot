FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_6.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PhotoGallery.csproj", ""]
RUN dotnet restore "PhotoGallery.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "PhotoGallery.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PhotoGallery.csproj" -c Release -o /app

FROM base AS final
ENV ASPNETCORE_URLS http://*:5000 
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PhotoGallery.dll"]