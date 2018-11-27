FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /src
COPY EverisLdap.csproj ./
run dotnet add package System.DirectoryServices.AccountManagement --version 4.6.0-preview1-27018-01 --source https://dotnet.myget.org/F/dotnet-core/api/v3/index.json
run dotnet add package System.DirectoryServices --version 4.6.0-preview1-27018-01 --source https://dotnet.myget.org/F/dotnet-core/api/v3/index.json

RUN dotnet restore EverisLdap.csproj
COPY . .
WORKDIR /src/
RUN dotnet build EverisLdap.csproj -c Release -o /app

FROM build AS publish
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
RUN dotnet publish EverisLdap.csproj -c Release -o /app

FROM base AS final
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EverisLdap.dll"]
