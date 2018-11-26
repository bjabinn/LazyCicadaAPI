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
COPY LazyCicadaAPI.csproj ./
RUN dotnet restore LazyCicadaAPI.csproj
COPY . .
WORKDIR /src/
RUN dotnet build LazyCicadaAPI.csproj -c Release -o /app

FROM build AS publish
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
RUN dotnet publish LazyCicadaAPI.csproj -c Release -o /app

FROM base AS final
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LazyCicadaAPI.dll"]
