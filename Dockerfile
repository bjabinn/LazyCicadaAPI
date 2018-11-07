FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /src
COPY LazyCicadaApi.csproj ./
RUN dotnet restore LazyCicadaApi.csproj
COPY . .
WORKDIR /src/
RUN dotnet build LazyCicadaApi.csproj -c Release -o /app

FROM build AS publish
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
RUN dotnet publish LazyCicadaApi.csproj -c Release -o /app

FROM base AS final
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LazyCicadaApi.dll"]
