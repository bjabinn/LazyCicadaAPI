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
COPY LoginCicada.csproj ./
RUN dotnet restore LoginCicada.csproj
COPY . .
WORKDIR /src/
RUN dotnet build LoginCicada.csproj -c Release -o /app

FROM build AS publish
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
RUN dotnet publish LoginCicada.csproj -c Release -o /app

FROM base AS final
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LoginCicada.dll"]
