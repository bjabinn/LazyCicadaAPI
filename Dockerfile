FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 53711
EXPOSE 44356

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY LazyCicadaApi.csproj ./
RUN dotnet restore /LazyCicadaApi.csproj
COPY . .
WORKDIR /src/
RUN dotnet build LazyCicadaApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish LazyCicadaApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LazyCicadaApi.dll"]
