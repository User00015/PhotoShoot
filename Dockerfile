FROM microsoft/dotnet:2.1-sdk as build-env
WORKDIR /app
#setup node

# copy csproj and restore as distinct layers
WORKDIR "/src"
COPY ["PhotoGallery.csproj", ""]
RUN dotnet restore "PhotoGallery.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "PhotoGallery.csproj" -c Release -o /app

ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs \
	&& npm i -g @angular/cli

RUN dotnet publish "PhotoGallery.csproj" -c Release -o /app

# build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
ENV ASPNETCORE_URLS http://*:5000 
WORKDIR /app
#setup node, this is only needed if you use Node both at runtime and build time. Some people may only need the build part.
ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs \
	&& npm i -g @angular/cli
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "PhotoGallery.dll"]

## Setup NodeJs
#RUN apt-get update && \
    #apt-get install -y wget && \
    #apt-get install -y gnupg2 && \
    #wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    #apt-get install -y build-essential nodejs
## End setup
#
#WORKDIR /app
#EXPOSE 32772
#
#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /src
#COPY ["PhotoGallery.csproj", ""]
#RUN dotnet restore "PhotoGallery.csproj"
#
#COPY . .
#WORKDIR "/src/"
#RUN dotnet build "PhotoGallery.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "PhotoGallery.csproj" -c Release -o /app
#
#FROM base AS final
#ENV ASPNETCORE_URLS http://*:5000 
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "PhotoGallery.dll"]

#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#
## Setup NodeJs
#RUN apt-get update && \
    #apt-get install -y wget && \
    #apt-get install -y gnupg2 && \
    #wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    #apt-get install -y build-essential nodejs && \
	#apt-get install -y yarn
## End setup
#
#WORKDIR /app
#



#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /src
#COPY ["PhotoGallery.csproj", ""]
#RUN dotnet restore "PhotoGallery.csproj"
#COPY . .
#WORKDIR "/src/"
#RUN dotnet build "PhotoGallery.csproj" -c Release -o /app
#
#FROM build AS publish
#
#RUN apt-get update && \
    #apt-get install -y wget && \
    #apt-get install -y gnupg2 && \
    #wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    #apt-get install -y build-essential nodejs && \
	#apt-get install -y yarn
#RUN yarn
#RUN dotnet publish "PhotoGallery.csproj" -c Release -o /app
#
#FROM base AS final
#ENV ASPNETCORE_URLS http://*:5000 
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "PhotoGallery.dll"]