FROM microsoft/aspnetcore
WORKDIR /api
COPY . .
EXPOSE 8084/tcp
ENTRYPOINT ["dotnet", "LazyCicadaAPI.dll"]