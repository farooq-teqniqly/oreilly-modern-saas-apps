FROM mcr.microsoft.com/devcontainers/dotnet:8.0
RUN su vscode -c "umask 0002 && . /usr/local/share/nvm/nvm.sh && nvm install 20 2>&1"
RUN apt-get update && \
    export DEBIAN_FRONTEND=noninteractive && \
    apt-get -qy full-upgrade && \
    apt-get install -qy curl && \
    apt-get -y install --no-install-recommends vim && \
    curl -sSL https://get.docker.com | sh
RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet dev-certs https